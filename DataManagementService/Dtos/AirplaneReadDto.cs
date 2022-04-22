using System.ComponentModel.DataAnnotations;
using DataManagementService.Models;

namespace DataManagementService.Dtos;

public class AirplaneReadDto
{
	[Required] public int Id { get; set; }
	[Required] public string SerialNumber { get; set; } = "";
	[Required] public int TotalFlights { get; set; }
	[Required] public int AvailableSeats { get; set; }
	[Required] public int AirplaneVariantId { get; set; }
}
