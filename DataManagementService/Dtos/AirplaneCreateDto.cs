using System.ComponentModel.DataAnnotations;

namespace DataManagementService.Dtos;

public class AirplaneCreateDto
{
	[Required] public string SerialNumber { get; set; } = "";
	[Required] public int VariantId { get; set; }
	[Required] public int AvailableSeats { get; set; }
}
