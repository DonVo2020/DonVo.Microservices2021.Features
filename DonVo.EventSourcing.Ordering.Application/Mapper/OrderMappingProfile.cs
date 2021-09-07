using AutoMapper;
using DonVo.EventSourcing.Ordering.Application.Commands.OrderCreate;
using DonVo.EventSourcing.Ordering.Application.Responses;
using DonVo.EventSourcing.Ordering.Domain.Entities;

namespace DonVo.EventSourcing.Ordering.Application.Mapper
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderCreateCommand>().ReverseMap();
            CreateMap<Order, OrderResponse>().ReverseMap();
        }
    }
}
