using DataManagementService.Dtos;

namespace DataManagementService.AsyncDataServices;

public interface IMessageBusClient
{
	void PublishAirplane(AirplanePublishDto airplanePublishDto);
}
