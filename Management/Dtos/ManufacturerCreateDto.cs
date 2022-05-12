using System.ComponentModel.DataAnnotations;

namespace Management.Dtos;

public class ManufacturerCreateDto
{
	[Required] public string Company { get; set; } = "";
	[Required] public int CountryId { get; set; }
}
