namespace DataManagementService.Models;

public class AirplaneVariant
{
	public int Id { get; set; }
	public string Model { get; set; } = "";
	public Status Status { get; set; }

	public int ManufacturerId { get; init; }
	public Manufacturer Manufacturer { get; set; } = null!;
}

public enum Status
{
	InService,
	InLimitedService,
	Retired
}
