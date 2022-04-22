using System.ComponentModel.DataAnnotations;

namespace DataManagementService.Dtos;

public class ManufacturerCreateDto
{
	[Required] public string Company { get; set; } = "";
	[Required] public int CountryId { get; set; }
}
