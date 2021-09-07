﻿using AutoMapper;
using DonVo.EventSourcing.EventBusRabbitMQ.Core;
using DonVo.EventSourcing.EventBusRabbitMQ.Events;
using DonVo.EventSourcing.EventBusRabbitMQ.Producer;
using DonVo.EventSourcing.MongoDB.SourcingAPI.Entities;
using DonVo.EventSourcing.MongoDB.SourcingAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DonVo.EventSourcing.MongoDB.SourcingAPI.Controllers
{
    public class AuctionController : BaseController
    {
        #region Fields
        private readonly IAuctionRepository _auctionRepository;
        private readonly IBidRepository _bidRepository;
        private readonly EventBusRabbitMQProducer _eventBusRabbitMQProducer;
        private readonly ILogger<AuctionController> _logger;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public AuctionController(
            IAuctionRepository auctionRepository,
            IBidRepository bidRepository,
            EventBusRabbitMQProducer eventBusRabbitMQProducer,
            ILogger<AuctionController> auctionLogger,
            IMapper mapper)
        {
            _auctionRepository = auctionRepository;
            _bidRepository = bidRepository;
            _eventBusRabbitMQProducer = eventBusRabbitMQProducer;
            _logger = auctionLogger;
            _mapper = mapper;
        }
        #endregion

        #region Methods
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Auction>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAuctions()
        {
            var auctions = await _auctionRepository.GetAuctionsAsync();
            if (auctions is null || !auctions.Any())
            {
                return NotFound();
            }

            return Ok(auctions);
        }

        [HttpGet("{id:minlength(24)}")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAuctionById(string id)
        {
            var auction = await _auctionRepository.GetAuctionByIdAsync(id);
            if (auction is null)
            {
                _logger.LogError($"Auction with id: {id}, hasn't been found in database.");
                return NotFound();
            }

            return Ok(auction);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAuction([FromBody] Auction auction)
        {
            await _auctionRepository.InsertAsync(auction);
            return CreatedAtRoute(nameof(GetAuctionById), new { id = auction.Id }, auction);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> UpdateAuction([FromBody] Auction auction)
        {
            bool isSuccess = await _auctionRepository.UpdateAsync(auction);
            if (!isSuccess)
            {
                return Problem();
            }

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Auction), (int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> DeleteAuctionById(string id)
        {
            bool isSuccess = await _auctionRepository.DeleteAsync(id);
            if (!isSuccess)
            {
                return Problem();
            }

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        public async Task<IActionResult> CompleteAuction([FromBody] string id)
        {
            var auction = await _auctionRepository.GetAuctionByIdAsync(id);
            if (auction is null)
                return NotFound();

            if (auction.Status != (int)Status.Active)
            {
                _logger.LogError("Auction can not be completed");
                return BadRequest();
            }

            var bid = await _bidRepository.GetAuctionWinnigBidByAuctionIdAsync(id);
            if (bid is null)
                return NotFound();

            OrderCreateEvent eventMessage = _mapper.Map<OrderCreateEvent>(bid);
            eventMessage.Quantity = auction.Quantity;

            auction.Status = (int)Status.Closed;
            bool updateResponse = await _auctionRepository.UpdateAsync(auction);
            if (!updateResponse)
            {
                _logger.LogError("Auction can not updated");
                return BadRequest();
            }

            try
            {
                _eventBusRabbitMQProducer.Publish(EventBusConstants.OrderCreateQueue, eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.Id, "Sourcing");
                throw;
            }

            return Accepted();
        }

        [HttpPost]
        public IActionResult TestEvent()
        {
            OrderCreateEvent eventMessage = new()
            {
                AuctionId = "dummy1",
                ProductId = "dummy_product_1",
                Price = 10,
                Quantity = 100,
                SellerUserName = "test@test.com"
            };

            try
            {
                _eventBusRabbitMQProducer.Publish(EventBusConstants.OrderCreateQueue, eventMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", eventMessage.Id, "Sourcing");
                throw;
            }

            return Accepted(eventMessage);
        }
        #endregion
    }
}
