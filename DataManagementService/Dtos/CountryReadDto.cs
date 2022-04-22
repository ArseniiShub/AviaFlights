using System.ComponentModel.DataAnnotations;

namespace DataManagementService.Dtos;

public class CountryReadDto
{
	[Required] public int Id { get; set; }
	[Required] public string Name { get; set; } = "";
}
