using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMqNet5.ExchangeTypes.Consumer
{
    public static class FanoutExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("donvo-fanout-exchange", ExchangeType.Fanout);
            channel.QueueDeclare("donvo-fanout-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);


            channel.QueueBind("donvo-fanout-queue", "donvo-fanout-exchange", string.Empty);
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("donvo-fanout-queue", true, consumer);
            Console.WriteLine("Consumer started");
            Console.ReadLine();
        }
    }
}