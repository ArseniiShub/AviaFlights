using System.ComponentModel.DataAnnotations;

namespace Management.Dtos;

public class CountryReadDto
{
	[Required] public int Id { get; set; }
	[Required] public string Name { get; set; } = "";
}
