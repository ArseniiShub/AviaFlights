namespace AirplanesService.AsyncDataServices;

public interface IMessageBusClient
{
	void PublishNewAirplane(AirplanePublishDto airplanePublishDto);
}
