namespace FlightCatalogService.Services;

public interface IMessageBusEventsProcessor
{
	void ProcessEvent(string jsonNotification);
}
