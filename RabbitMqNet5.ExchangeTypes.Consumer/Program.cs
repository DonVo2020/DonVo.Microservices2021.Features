using RabbitMQ.Client;
using System;

namespace RabbitMqNet5.ExchangeTypes.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqps://nssuexqa:HglymP0teINAqftI-dzGUAB9JPeBaojs@clam.rmq.cloudamqp.com/nssuexqa")
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            //FanoutExchangeConsumer.Consume(channel);
            //DirectExchangeConsumer.Consume(channel);
            //HeaderExchangeConsumer.Consume(channel);
            TopicExchangeConsumer.Consume(channel);
            //QueueConsumer.Consume(channel);
        }
    }
}
