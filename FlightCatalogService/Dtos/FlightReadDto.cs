using System.ComponentModel.DataAnnotations;

namespace FlightCatalogService.Dtos;

public class FlightReadDto
{
	[Required] public int Id { get; set; }
	[Required] public DateTime Departure { get; set; }
	[Required] public DateTime Arrival { get; set; }
	[Required] public int AvailableSeats { get; set; }
	[Required] public string AirplaneName { get; init; } = "";
	[Required] public string FlightRouteName { get; set; } = "";
}
