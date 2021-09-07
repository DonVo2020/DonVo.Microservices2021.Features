using AutoMapper;
using MediatR;
using DonVo.EventSourcing.Ordering.Application.Commands.OrderCreate;
using DonVo.EventSourcing.Ordering.Application.Responses;
using DonVo.EventSourcing.Ordering.Domain.Entities;
using DonVo.EventSourcing.Ordering.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DonVo.EventSourcing.Ordering.Application.Handlers
{
    public class OrderCreateHandler : IRequestHandler<OrderCreateCommand, OrderResponse>
    {
        #region Fields
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public OrderCreateHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<OrderResponse> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            if (orderEntity is null)
            {
                throw new ArgumentNullException(nameof(orderEntity), "Entity could not be mapped!");
            }

            var order = await _orderRepository.AddAsync(orderEntity);
            var orderResponse = _mapper.Map<OrderResponse>(order);
            return orderResponse;
        }
    }
}
