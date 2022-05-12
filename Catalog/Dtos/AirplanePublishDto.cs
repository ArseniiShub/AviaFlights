namespace Catalog.Dtos;

public class AirplanePublishDto
{
	public int Id { get; set; }
	public string ManufacturerName { get; set; } = "";
	public string ModelName { get; set; } = "";
	public string SerialNumber { get; set; } = "";
	public int AvailableSeats { get; set; }
}
