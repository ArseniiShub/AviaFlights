namespace Catalog.Models;

public class Flight
{
	public int Id { get; set; }
	public DateTime Departure { get; set; }
	public DateTime Arrival { get; set; }
	public int AvailableSeats { get; set; }

	public int AirplaneId { get; init; }
	public Airplane Airplane { get; set; } = null!;

	public int FlightRouteId { get; init; }
	public FlightRoute FlightRoute { get; set; } = null!;
}
