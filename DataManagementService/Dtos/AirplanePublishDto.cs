using DataManagementService.Enums;

namespace DataManagementService.Dtos;

public class AirplanePublishDto
{
	public int Id { get; set; }
	public string ManufacturerName { get; set; } = "";
	public string ModelName { get; set; } = "";
	public string SerialNumber { get; set; } = "";
	public int TotalFlights { get; set; }
	public int AvailableSeats { get; set; }
	public EventType EventType { get; set; }
}
