using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands.AddCommands;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Commands.RemoveCommands;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Extensions;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Params;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Requests;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Models.Responses;
using DonVo.CQRS.SignalR.BogusMoqTest.Core.Queries;

namespace DonVo.CQRS.SignalR.BogusMoqTest.API.Controllers
{
    public class MessagesController : BaseApiController
    {
        private readonly IMediator _mediator;

        public MessagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[HttpPost]
        //public async Task<ActionResult<MessageResponse>> AddMessage([FromBody]AddMessageRequest addMessageRequest, CancellationToken cancellationToken)
        //{
        //    var message = await _mediator.Send(
        //        new AddMessage.Command(User.GetUserId(), addMessageRequest.RecipientEmail, addMessageRequest.Content),cancellationToken);
    
        //    return CreatedAtRoute(nameof(GetMessages), new MessageParams { FilterBy = "Outbox" }, message);
        //}

        [HttpGet(Name = nameof(GetMessages))]
        public async Task<ActionResult<IEnumerable<MessageResponse>>> GetMessages([FromQuery]MessageParams messageParams, CancellationToken cancellationToken)
        {
            var messages = await _mediator.Send(
                new GetMessages.Query(User.GetUserId(), messageParams), cancellationToken);

            return Ok(messages);
        }

        [HttpGet("{userId}/thread")]
        public async Task<ActionResult<IEnumerable<MessageResponse>>> GetMessageThread(string userId, CancellationToken cancellationToken)
        {
            var messages = await _mediator.Send(
                new GetMessageThread.Query(User.GetUserId(), userId), cancellationToken);

            return Ok(messages);
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{messageId}")]
        public async Task<ActionResult> RemoveMessage(int messageId)
        {
            await _mediator.Send(new RemoveMessage.Command(messageId, User.GetUserId()));
            return NoContent();
        }

    }
}
