using RabbitMQ.Client;
using System;

namespace RabbitMqNet5.ExchangeTypes.Producer
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
            //FanoutExchangePublisher.Publish(channel);
            //DirectExchangePublisher.Publish(channel);
            //HeaderExchangePublisher.Publish(channel);
            TopicExchangePublisher.Publish(channel);
            //QueueProducer.Publish(channel);
        }
    }
}
