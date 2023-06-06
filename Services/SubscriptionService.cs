using System.Text;
using System.Text.Json;
using desafio.Repository.IRepository;
using desafio.Services.IServices;
using RabbitMQ.Client;

namespace desafio.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private IRepositorySubscriptions _repository;
        public SubscriptionService(IRepositorySubscriptions repositorySubscription)
        {
            _repository = repositorySubscription;
        }
        public async Task SendingMessage<T>(T message)
        {
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

            var jsonString = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonString);

            channel.BasicPublish(exchange:"", routingKey:"SubscriptionQueue", body: body);
            await _repository.SaveSubscription();
        }
    }
}