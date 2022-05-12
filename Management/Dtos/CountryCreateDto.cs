using System.ComponentModel.DataAnnotations;

namespace Management.Dtos;

public class CountryCreateDto
{
	[Required] public string Name { get; set; } = "";
}
