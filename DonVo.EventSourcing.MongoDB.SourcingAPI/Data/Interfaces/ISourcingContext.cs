using DonVo.EventSourcing.MongoDB.SourcingAPI.Entities;
using MongoDB.Driver;

namespace DonVo.EventSourcing.MongoDB.SourcingAPI.Data.Interfaces
{
    public interface ISourcingContext
    {
        IMongoCollection<Auction> Auctions { get; }
        IMongoCollection<Bid> Bids { get; }
    }
}
