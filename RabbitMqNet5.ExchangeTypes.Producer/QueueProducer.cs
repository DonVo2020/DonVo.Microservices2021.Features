using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Threading;

namespace RabbitMqNet5.ExchangeTypes.Producer
{
    public class QueueProducer
    {
        public static void Publish(IModel channel)
        {
            channel.QueueDeclare("donvo-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = "Hello Don Vo - Queue! Count: {" + count + "}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("", "donvo-queue", null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
