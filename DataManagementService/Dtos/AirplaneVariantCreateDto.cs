using System.ComponentModel.DataAnnotations;
using DataManagementService.Models;

namespace DataManagementService.Dtos;

public class AirplaneVariantCreateDto
{
	[Required] public int ManufacturerId { get; set; }
	[Required] public string Model { get; set; } = "";
	[Required] public Status Status { get; set; }
}
