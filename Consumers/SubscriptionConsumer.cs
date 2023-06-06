using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace desafio.Consumers
{
    public class SubscriptionConsumer
    {
        public string message { get; set; } = string.Empty;
        public async Task<string> Receiver() {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };

            var connection = factory.CreateConnection();

            using var channel = connection.CreateModel();

            channel.QueueDeclare("SubscriptionQueue", durable: true, exclusive: false, autoDelete: false);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, eventArgs) =>
            {
                //Array de bytes
                var body = eventArgs.Body.ToArray();
                message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"{message}");
            };
            
            channel.BasicConsume("SubscriptionQueue", true,  consumer);
            await Task.Delay(TimeSpan.FromSeconds(1));
            return message;
        }
    }
}