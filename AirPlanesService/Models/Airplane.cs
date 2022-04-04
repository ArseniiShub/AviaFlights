namespace AirplanesService.Models;

public class Airplane
{
	public int Id { get; set; }
	public Manufacturer Manufacturer { get; set; } = null!;
	public int ManufacturerId { get; set; }
	public string Model { get; set; } = "";
	public Status Status { get; set; }
}

public enum Status
{
	InService,
	InLimitedService,
	Retired
}
