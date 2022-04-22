using System.Text;
using FlightCatalogService.ConstantValues;
using FlightCatalogService.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FlightCatalogService.AsyncDataServices;

public class RabbitMQMessageBusSubscriber : BackgroundService
{
	private readonly ILogger<RabbitMQMessageBusSubscriber> _logger;
	private readonly IMessageBusEventsProcessor _eventProcessor;
	private readonly IConnection _connection;
	private readonly IModel _channel;

	public RabbitMQMessageBusSubscriber(IConfiguration configuration, ILogger<RabbitMQMessageBusSubscriber> logger,
		IMessageBusEventsProcessor eventProcessor)
	{
		_logger = logger;
		_eventProcessor = eventProcessor;
		
		var factory = new ConnectionFactory
		{
			HostName = configuration[ConfigKeyPaths.RabbitMQHost],
			Port = int.Parse(configuration[ConfigKeyPaths.RabbitMQPort])
		};

		_connection = factory.CreateConnection();
		_channel = _connection.CreateModel();
		_channel.QueueDeclare(ConfigKeyPaths.CatalogManagementPublishQueue, true, false, false);
		_connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
	}

	private void RabbitMQ_ConnectionShutdown(object? sender, ShutdownEventArgs e)
	{
		_logger.LogInformation("RabbitMQ connection shutdown");
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		stoppingToken.ThrowIfCancellationRequested();

		var consumer = new EventingBasicConsumer(_channel);
		consumer.Received += OnEventReceived;
		_logger.LogInformation("Listening RabbitMQ events");

		return Task.CompletedTask;
	}

	private void OnEventReceived(object? sender, BasicDeliverEventArgs e)
	{
		_logger.LogInformation("Event received");

		var body = e.Body;
		var jsonNotification = Encoding.UTF8.GetString(body.ToArray());

		_eventProcessor.ProcessEvent(jsonNotification);
	}

	public override void Dispose()
	{
		_channel.Close();
		_connection.Close();

		base.Dispose();
	}
}
