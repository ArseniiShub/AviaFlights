namespace Management.Models;

public class Airplane
{
	public int Id { get; set; }
	public string SerialNumber { get; set; } = "";
	public int TotalFlights { get; set; }
	public int AvailableSeats { get; set; }
	public int VariantId { get; init; }
	public AirplaneVariant Variant { get; set; } = null!;
}
