using System.ComponentModel.DataAnnotations;

namespace Management.Dtos;

public class FlightRouteCreateDto
{
	[Required] public string Name { get; set; } = "";
	[Required] public int FromAirportId { get; set; }
	[Required] public int ToAirportId { get; set; }
}
