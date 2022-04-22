using System.ComponentModel.DataAnnotations;
using DataManagementService.Models;

namespace DataManagementService.Dtos;

public class AirplaneVariantReadDto
{
	[Required] public int Id { get; set; }
	[Required] public ManufacturerReadDto Manufacturer { get; set; } = null!;
	[Required] public string Model { get; set; } = "";
	[Required] public Status Status { get; set; }
}
