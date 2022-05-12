using System.ComponentModel.DataAnnotations;

namespace Management.Dtos;

public class AirplaneCreateDto
{
	[Required] public string SerialNumber { get; set; } = "";
	[Required] public int VariantId { get; set; }
	[Required] public int AvailableSeats { get; set; }
}
