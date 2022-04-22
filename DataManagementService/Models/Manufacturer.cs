// ReSharper disable CollectionNeverUpdated.Global
namespace DataManagementService.Models;

public class Manufacturer
{
	public int Id { get; set; }
	public string Company { get; set; } = "";

	public int CountryId { get; init; }
	public Country Country { get; set; } = null!;

	public ICollection<AirplaneVariant> AirplaneVariants { get; set; } = new List<AirplaneVariant>();
}
