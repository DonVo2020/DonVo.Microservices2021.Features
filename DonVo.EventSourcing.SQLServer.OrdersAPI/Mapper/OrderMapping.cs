using AutoMapper;
using DonVo.EventSourcing.EventBusRabbitMQ.Events;
using DonVo.EventSourcing.Ordering.Application.Commands.OrderCreate;

namespace DonVo.EventSourcing.SQLServer.OrdersAPI.Mapper
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderCreateEvent, OrderCreateCommand>().ReverseMap();
        }
    }
}
