using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DonVo.EventSourcing.Ordering.Domain.Repositories;
using DonVo.EventSourcing.Ordering.Domain.Repositories.Base;
using DonVo.EventSourcing.Ordering.Infrastructure.Data;
using DonVo.EventSourcing.Ordering.Infrastructure.Repositories;
using DonVo.EventSourcing.Ordering.Infrastructure.Repositories.Base;

namespace DonVo.EventSourcing.Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<OrderContext>(opt => opt.UseInMemoryDatabase("InMemoryDb"), ServiceLifetime.Singleton, ServiceLifetime.Singleton);

            services.AddDbContext<OrderContext>(opt =>  opt.UseSqlServer(configuration.GetConnectionString("OrderApiConnection"),
                                                sqlServerOptionsAction => sqlServerOptionsAction.MigrationsAssembly(typeof(OrderContext).Assembly.FullName)), ServiceLifetime.Singleton);

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>)); 
            services.AddTransient<IOrderRepository, OrderRepository>();
        }
    }
}
