namespace AirplanesService.Models;

public class Manufacturer
{
	public int Id { get; set; }
	public string Company { get; set; } = "";
	public string Country { get; set; } = "";

	public ICollection<Airplane> Airplanes { get; set; } = new List<Airplane>();
}
