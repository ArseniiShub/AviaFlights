using System.ComponentModel.DataAnnotations;

namespace Management.Dtos;

public class AirportReadDto
{
	[Required] public int Id { get; set; }
	[Required] public string Name { get; set; } = "";
	public string? City { get; set; }
	[Required] public double Latitude { get; set; }
	[Required] public double Longitude { get; set; }
	[Required] public int CountryId { get; set; }
}
