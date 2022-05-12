namespace Catalog.Services;

public interface IMessageBusEventsProcessor
{
	void ProcessEvent(string jsonNotification);
}
