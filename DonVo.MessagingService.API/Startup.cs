using DonVo.MessagingService.API.Helpers;
using DonVo.MessagingService.API.Services;
using DonVo.MessagingService.Domain.Repositories;
using DonVo.MessagingService.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

namespace DonVo.DonVo.MessagingService.API
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DonVo.MessagingService.API", Version = "v1" });

                var filePath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "DonVo.MessagingService.API.xml");
                c.IncludeXmlComments(filePath);
            });
            
            services.AddScoped<IMongoClient>(x => new MongoClient(connectionString: Configuration.GetConnectionString("MongoDB")));

            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ILogger, ElasticSearchLogger>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger logger)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DonVo.MessagingService.API v1"));

            app.UseRouting();

            app.UseAuthorization();

            app.ConfigureExceptionHandler(logger);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
