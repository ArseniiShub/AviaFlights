using System.Text;
using System.Text.Json;
using Management.ConstantValues;
using Management.Dtos;
using RabbitMQ.Client;

namespace Management.AsyncDataServices;

public class RabbitMQMessageBusClient : IMessageBusClient, IDisposable
{
	private readonly ILogger<RabbitMQMessageBusClient> _logger;

	private readonly string _queueName;
	private readonly IConnection _connection;
	private readonly IModel _channel;

	public RabbitMQMessageBusClient(IConfiguration configuration, ILogger<RabbitMQMessageBusClient> logger)
	{
		_logger = logger;
		var factory = new ConnectionFactory
		{
			HostName = configuration[ConfigKeyPaths.RabbitMQHost],
			Port = int.Parse(configuration[ConfigKeyPaths.RabbitMQPort])
		};

		_queueName = configuration[ConfigKeyPaths.ManagementPublishQueue];
		_connection = factory.CreateConnection();
		_channel = _connection.CreateModel();
		_channel.QueueDeclare(_queueName, true, false, false);
		_connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

		_logger.LogInformation("RabbitMQMessageBusClient initialized");
	}

	private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
	{
		_logger.LogInformation("RabbitMQ connection shutdown");
	}

	public void PublishAirplane(AirplanePublishDto airplanePublishDto)
	{
		_logger.LogInformation("Publishing Airplane with id {Id}...", airplanePublishDto.Id);

		if(_connection.IsOpen)
		{
			var message = JsonSerializer.Serialize(airplanePublishDto);
			var body = Encoding.UTF8.GetBytes(message);
			_channel.BasicPublish("", _queueName, null, body);

			_logger.LogInformation("Airplane with id {Id} published", airplanePublishDto.Id);
		}
		else
		{
			_logger.LogWarning("Could not publish airplane. Connection is closed");
		}
	}

	public void Dispose()
	{
		_channel.Close();
		_connection.Close();

		_logger.LogInformation("RabbitMQMessageBusClient disposed");
	}
}
