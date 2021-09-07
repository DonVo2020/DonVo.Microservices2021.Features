using DonVo.EventSourcing.MongoDB.SourcingAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.EventSourcing.MongoDB.SourcingAPI.Repositories.Interfaces
{
    public interface IBidRepository
    {
        Task SendBidAsync(Bid bid);
        Task<List<Bid>> GetBidsByAuctionIdAsync(string id);
        Task<List<Bid>> GetAllBidsByAuctionIdAsync(string id);
        Task<Bid> GetAuctionWinnigBidByAuctionIdAsync(string auctionId);
    }
}
