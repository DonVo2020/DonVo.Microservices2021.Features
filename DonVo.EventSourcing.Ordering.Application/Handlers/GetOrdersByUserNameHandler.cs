using AutoMapper;
using MediatR;
using DonVo.EventSourcing.Ordering.Application.Queries;
using DonVo.EventSourcing.Ordering.Application.Responses;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DonVo.EventSourcing.Ordering.Domain.Repositories;

namespace DonVo.EventSourcing.Ordering.Application.Handlers
{
    public class GetOrdersByUserNameHandler : IRequestHandler<GetOrdersBySellerUsernameQuery, IEnumerable<OrderResponse>>
    {
        #region Fields
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        #endregion

        #region Ctor
        public GetOrdersByUserNameHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }
        #endregion

        public async Task<IEnumerable<OrderResponse>> Handle(GetOrdersBySellerUsernameQuery request, CancellationToken cancellationToken)
        {
            var orderList = await _orderRepository.GetOrdersBySellerUserNameAsync(request.UserName);
            var orderResponseList = _mapper.Map<IEnumerable<OrderResponse>>(orderList);
            return orderResponseList;
        }
    }
}
