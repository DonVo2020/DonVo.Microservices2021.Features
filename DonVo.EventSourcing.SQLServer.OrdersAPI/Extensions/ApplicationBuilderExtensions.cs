using DonVo.EventSourcing.SQLServer.OrdersAPI.Consumers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace DonVo.EventSourcing.SQLServer.OrdersAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        #region Fields
        public static EventBusOrderCreateConsumer Listener { get; set; }
        #endregion

        #region Public Methods
        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusOrderCreateConsumer>();
            var hostApplicationLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
            hostApplicationLifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }
        #endregion

        #region Private Methods
        private static void OnStarted()
        {
            Listener.Consume();
        }

        private static void OnStopping()
        {
            Listener.Disconnect();
        }
        #endregion
    }
}
