namespace FlightCatalogService.Models;

public class FlightRoute
{
	public int Id { get; set; }
	public int ExternalId { get; set; }
	public string Name { get; set; } = "";

	public int FromAirportId { get; init; }
	public Airport FromAirport { get; set; } = null!;

	public int ToAirportId { get; init; }
	public Airport ToAirport { get; set; } = null!;
}
