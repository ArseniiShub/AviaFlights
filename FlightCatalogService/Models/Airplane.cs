namespace FlightCatalogService.Models;

public class Airplane
{
	public int Id { get; set; }
	public int ExternalId { get; set; }
	public string SerialNumber { get; set; } = "";
	public string AirplaneVariantName { get; set; } = "";
	public int AvailableSeats { get; set; }
}
