﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DonVo.EventSourcing.Ordering.Infrastructure.Data;
using System;

namespace DonVo.EventSourcing.SQLServer.OrdersAPI.Extensions
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using var serviceScope = host.Services.CreateScope();
            try
            {
                OrderContext orderContext = serviceScope.ServiceProvider.GetRequiredService<OrderContext>();

                if (orderContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                {
                    orderContext.Database.Migrate();
                }

                OrderContextSeed.SeedAsync(orderContext).Wait();
            }
            catch (Exception)
            {
                throw;
            }

            return host;
        }
    }
}
