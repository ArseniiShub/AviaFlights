using System.ComponentModel.DataAnnotations;

namespace AirplanesService.Dtos;

public class ManufacturerCreateDto
{
	[Required] public string Company { get; set; } = "";
	[Required] public string Country { get; set; } = "";
}
