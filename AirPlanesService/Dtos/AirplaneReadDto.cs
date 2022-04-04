using System.ComponentModel.DataAnnotations;

namespace AirplanesService.Dtos;

public class AirplaneReadDto
{
	[Required] public int Id { get; set; }
	[Required] public ManufacturerReadDto Manufacturer { get; set; } = null!;
	[Required] public string Model { get; set; } = "";
	[Required] public Status Status { get; set; }
}
