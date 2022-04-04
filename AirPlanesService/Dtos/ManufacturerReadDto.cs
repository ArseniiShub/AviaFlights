using System.ComponentModel.DataAnnotations;

namespace AirplanesService.Dtos;

public class ManufacturerReadDto
{
	[Required] public int Id { get; set; }
	[Required] public string Company { get; set; } = "";
	[Required] public string Country { get; set; } = "";
}
