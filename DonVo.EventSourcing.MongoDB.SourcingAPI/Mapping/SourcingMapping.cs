using AutoMapper;
using DonVo.EventSourcing.MongoDB.SourcingAPI.Entities;
using DonVo.EventSourcing.EventBusRabbitMQ.Events;

namespace DonVo.EventSourcing.MongoDB.SourcingAPI.Mapping
{
    public class SourcingMapping : Profile
    {
        public SourcingMapping()
        {
            AuctionMapping();
        }

        private void AuctionMapping()
        {
            CreateMap<OrderCreateEvent, Bid>().ReverseMap();
        }
    }
}
