using DonVo.EventSourcing.Ordering.Domain.Entities;
using DonVo.EventSourcing.Ordering.Domain.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.EventSourcing.Ordering.Domain.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersBySellerUserNameAsync(string userName);
    }
}
