using Management.Dtos;

namespace Management.AsyncDataServices;

public interface IMessageBusClient
{
	void PublishAirplane(AirplanePublishDto airplanePublishDto);
}
