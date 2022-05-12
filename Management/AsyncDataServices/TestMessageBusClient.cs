using Management.Dtos;

namespace Management.AsyncDataServices;

public class TestMessageBusClient : IMessageBusClient
{
	private readonly ILogger<TestMessageBusClient> _logger;

	public TestMessageBusClient(ILogger<TestMessageBusClient> logger)
	{
		_logger = logger;
	}

	public void PublishAirplane(AirplanePublishDto airplanePublishDto)
	{
		_logger.LogInformation("Airplane published");
	}
}
