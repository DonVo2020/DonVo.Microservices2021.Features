using DonVo.EventSourcing.MongoDB.SourcingAPI.Data.Interfaces;
using DonVo.EventSourcing.MongoDB.SourcingAPI.Entities;
using DonVo.EventSourcing.MongoDB.SourcingAPI.Settings.SourcingDatabase;
using MongoDB.Driver;

namespace DonVo.EventSourcing.MongoDB.SourcingAPI.Data
{
    public class SourcingContext : ISourcingContext
    {
        #region Ctor
        public SourcingContext(ISourcingDatabaseSettings sourcingDatabaseSettings)
        {
            var mongoClient = new MongoClient(sourcingDatabaseSettings.ConnectionString);
            var database = mongoClient.GetDatabase(sourcingDatabaseSettings.DatabaseName);

            Auctions = database.GetCollection<Auction>(nameof(Auction));
            Bids = database.GetCollection<Bid>(nameof(Bid));

            SourcingContextSeed.SeedData(Auctions);
        }
        #endregion

        #region Mongo Collections
        public IMongoCollection<Auction> Auctions { get; }
        public IMongoCollection<Bid> Bids { get; }
        #endregion
    }
}
