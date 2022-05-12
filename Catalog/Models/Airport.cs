namespace Catalog.Models;

public class Airport
{
	public int Id { get; set; }
	public int ExternalId { get; set; }
	public string Name { get; set; } = "";
	public string? City { get; set; }
	public double Latitude { get; set; }
	public double Longitude { get; set; }

	public int CountryId { get; init; }
	public Country Country { get; set; } = null!;
}
