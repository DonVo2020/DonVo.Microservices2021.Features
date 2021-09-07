using DonVo.EventSourcing.MongoDB.ProductsAPI.Entities;
using MongoDB.Driver;

namespace DonVo.EventSourcing.MongoDB.ProductsAPI.Data.Interfaces
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
