using System.ComponentModel.DataAnnotations;

namespace Management.Dtos;

public class ManufacturerReadDto
{
	[Required] public int Id { get; set; }
	[Required] public string Company { get; set; } = "";
	[Required] public int CountryId { get; set; }
}
