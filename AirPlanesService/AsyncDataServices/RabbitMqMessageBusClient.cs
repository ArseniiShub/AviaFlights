using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace AirplanesService.AsyncDataServices;

public class RabbitMQMessageBusClient : IMessageBusClient, IDisposable
{
	private const string QueueName = "AirplanePublished";

	private readonly ILogger<RabbitMQMessageBusClient> _logger;
	private readonly IConnection _connection;
	private readonly IModel _channel;

	public RabbitMQMessageBusClient(IConfiguration configuration, ILogger<RabbitMQMessageBusClient> logger)
	{
		ArgumentNullException.ThrowIfNull(configuration);
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		var factory = new ConnectionFactory
		{
			HostName = configuration["RabbitMQ:Host"],
			Port = int.Parse(configuration["RabbitMQ:Port"])
		};

		_connection = factory.CreateConnection();
		_channel = _connection.CreateModel();
		_channel.QueueDeclare(QueueName, true, false, false);
		_connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

		_logger.LogInformation("Connected to MessageBus");
	}

	private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
	{
		_logger.LogInformation("RabbitMQ connection shutdown");
	}

	public void PublishNewAirplane(AirplanePublishDto airplanePublishDto)
	{
		_logger.LogInformation("Sending message...");

		if(_connection.IsOpen)
		{
			var message = JsonSerializer.Serialize(airplanePublishDto);
			var body = Encoding.UTF8.GetBytes(message);
			_channel.BasicPublish("", QueueName, null, body);
			_logger.LogInformation("Message sent");
		}
		else
		{
			_logger.LogWarning("RabbitMQ connection is closed. Cant send message");
		}
	}

	public void Dispose()
	{
		_logger.LogInformation("MessageBus disposed");

		_channel.Dispose();
		_connection.Dispose();
	}
}
