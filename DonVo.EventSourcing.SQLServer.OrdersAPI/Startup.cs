using DonVo.EventSourcing.SQLServer.OrdersAPI.Consumers;
using DonVo.EventSourcing.SQLServer.OrdersAPI.Extensions;
using DonVo.EventSourcing.EventBusRabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using DonVo.EventSourcing.Ordering.Application;
using DonVo.EventSourcing.Ordering.Infrastructure;
using RabbitMQ.Client;

namespace DonVo.EventSourcing.SQLServer.OrdersAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddInfrastructure(Configuration);
            services.AddApplication();
            services.AddAutoMapper(typeof(Startup));

            #region Swagger
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DonVo.EventSourcing.SQLServer.OrdersAPI",
                    Version = "1.0.0"
                });
            });
            #endregion

            #region RabbitMQ
            services.AddSingleton<IRabbitMQPersistentConnection>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                ConnectionFactory connectionFactory = new()
                {
                    HostName = Configuration["EventBusSettings:HostName"],
                    UserName = Configuration["EventBusSettings:UserName"],
                    Password = Configuration["EventBusSettings:Password"],
                    VirtualHost = Configuration["EventBusSettings:VirtualHostName"]
                };
                int retryCount = int.Parse(Configuration["EventBusSettings:RetryCount"]);

                return new DefaultRabbitMQPersistentConnection(connectionFactory, logger, retryCount);
            });

            services.AddSingleton<EventBusOrderCreateConsumer>();
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseRabbitListener();

            app.UseSwagger();
            app.UseSwaggerUI(setup => setup.SwaggerEndpoint("/swagger/v1/swagger.json", "DonVo.EventSourcing.SQLServer.OrdersAPI v1"));
        }
    }
}
