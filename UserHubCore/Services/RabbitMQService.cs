using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserDataHub.Core.DTOs;
using UserDataHub.Core.Interfaces;

public class RabbitMQService : IRabbitMQService
{
    private readonly ConnectionFactory _factory;
    private readonly string _queueName;

    public RabbitMQService(ConnectionFactory factory, string queueName)
    {
        _factory = factory;
        _queueName = queueName;
    }

    public async Task PublishAsync(UpdateUserInfoDto message)
    {
        using var connection = _factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

        var messageBody = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(messageBody);

        var properties = channel.CreateBasicProperties();
        properties.Persistent = true;

        await Task.Run(() => channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: properties, body: body));
    }
}
