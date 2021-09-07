using Microsoft.EntityFrameworkCore;
using DonVo.EventSourcing.Ordering.Domain.Entities;

namespace DonVo.EventSourcing.Ordering.Infrastructure.Data
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}
