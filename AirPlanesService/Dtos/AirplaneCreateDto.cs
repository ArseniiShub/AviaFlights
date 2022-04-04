using System.ComponentModel.DataAnnotations;

namespace AirplanesService.Dtos;

public class AirplaneCreateDto
{
	[Required] public int ManufacturerId { get; set; }
	[Required] public string Model { get; set; } = "";
	[Required] public Status Status { get; set; }
}
