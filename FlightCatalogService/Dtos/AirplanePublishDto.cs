namespace FlightCatalogService.Dtos;

public class AirplanePublishDto
{
	public int Id { get; set; }
	public string SerialNumber { get; set; } = "";
	public string AirplaneVariantName { get; set; } = "";
	public int AvailableSeats { get; set; }
}
