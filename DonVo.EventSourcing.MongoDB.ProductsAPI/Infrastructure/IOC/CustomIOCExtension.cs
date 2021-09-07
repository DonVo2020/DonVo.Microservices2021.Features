using DonVo.EventSourcing.MongoDB.ProductsAPI.Data;
using DonVo.EventSourcing.MongoDB.ProductsAPI.Data.Interfaces;
using DonVo.EventSourcing.MongoDB.ProductsAPI.Repositories;
using DonVo.EventSourcing.MongoDB.ProductsAPI.Repositories.Interfaces;
using DonVo.EventSourcing.MongoDB.ProductsAPI.Settings.ProductDatabase;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace DonVo.EventSourcing.MongoDB.ProductsAPI.Infrastructure.IOC
{
    public static class CustomIOCExtension
    {
        public static void AddSettingsConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configuration Dependencies
            services.Configure<ProductDatabaseSettings>(configuration.GetSection(nameof(ProductDatabaseSettings)));
            #endregion

            #region Singleton Service Dependencies
            services.AddSingleton<IProductDatabaseSettings>(provider => provider.GetRequiredService<IOptions<ProductDatabaseSettings>>().Value);
            #endregion
        }

        public static void AddServiceConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IProductContext, ProductContext>();
            services.AddTransient<IProductRepository, ProductRepository>();
        }

        public static void AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DonVo.EventSourcing.MongoDB.ProductsAPI",
                    Version = "1.0.0"
                });
            });
        }
    }
}
