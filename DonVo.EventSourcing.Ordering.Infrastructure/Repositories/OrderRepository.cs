using Microsoft.EntityFrameworkCore;
using DonVo.EventSourcing.Ordering.Domain.Entities;
using DonVo.EventSourcing.Ordering.Domain.Repositories;
using DonVo.EventSourcing.Ordering.Infrastructure.Data;
using DonVo.EventSourcing.Ordering.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.EventSourcing.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext orderContext) : base(orderContext)
        {
        }

        public async Task<IEnumerable<Order>> GetOrdersBySellerUserNameAsync(string userName)
        {
            var orderList = await _context.Orders.Where(p => p.SellerUserName.Equals(userName)).ToListAsync();
            return orderList;
        }
    }
}
