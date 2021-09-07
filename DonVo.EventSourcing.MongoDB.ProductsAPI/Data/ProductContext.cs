using DonVo.EventSourcing.MongoDB.ProductsAPI.Data.Interfaces;
using DonVo.EventSourcing.MongoDB.ProductsAPI.Entities;
using DonVo.EventSourcing.MongoDB.ProductsAPI.Settings.ProductDatabase;
using MongoDB.Driver;

namespace DonVo.EventSourcing.MongoDB.ProductsAPI.Data
{
    public class ProductContext : IProductContext
    {
        #region Ctor
        public ProductContext(IProductDatabaseSettings productDatabaseSettings)
        {
            var mongoClient = new MongoClient(productDatabaseSettings.ConnectionString);
            var database = mongoClient.GetDatabase(productDatabaseSettings.DatabaseName);

            Products = database.GetCollection<Product>(productDatabaseSettings.CollectionName);
            ProductContextSeed.SeedData(Products);
        }
        #endregion

        public IMongoCollection<Product> Products { get; }
    }
}
