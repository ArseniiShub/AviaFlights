using System.ComponentModel.DataAnnotations;

namespace Catalog.Dtos;

public class FlightReadDto
{
	[Required] public int Id { get; set; }
	[Required] public DateTime Departure { get; set; }
	[Required] public DateTime Arrival { get; set; }
	[Required] public int AvailableSeats { get; set; }
	[Required] public string AirplaneFullName { get; set; } = "";
	[Required] public string FlightRouteName { get; set; } = "";
}
