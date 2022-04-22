using System.ComponentModel.DataAnnotations;

namespace DataManagementService.Dtos;

public class FlightRouteReadDto
{
	[Required] public int Id { get; set; }
	[Required] public string Name { get; set; } = "";
	[Required] public int FromAirportId { get; set; }
	[Required] public int ToAirportId { get; set; }
}
