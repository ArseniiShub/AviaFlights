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
	private readonly string _queueName;

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
		_queueName = configuration[ConfigKeyPaths.DataManagementPublishQueue];

		_connection = factory.CreateConnection();
		_channel = _connection.CreateModel();
		_channel.QueueDeclare(_queueName, true, false, false);
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
		_channel.BasicConsume(_queueName, false, consumer);
		_logger.LogInformation("Listening RabbitMQ events");

		return Task.CompletedTask;
	}

	private void OnEventReceived(object? sender, BasicDeliverEventArgs e)
	{
		_logger.LogInformation("Event received");

		var body = e.Body;
		var jsonNotification = Encoding.UTF8.GetString(body.ToArray());

		_eventProcessor.ProcessEvent(jsonNotification);
		
		((EventingBasicConsumer)sender!).Model.BasicAck(e.DeliveryTag, false);
	}

	public override void Dispose()
	{
		_channel.Close();
		_connection.Close();

		base.Dispose();
	}
}
