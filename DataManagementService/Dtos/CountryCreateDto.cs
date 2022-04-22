using System.ComponentModel.DataAnnotations;

namespace DataManagementService.Dtos;

public class CountryCreateDto
{
	[Required] public string Name { get; set; } = "";
}
