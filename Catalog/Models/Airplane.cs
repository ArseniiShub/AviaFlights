namespace Catalog.Models;

public class Airplane
{
	public int Id { get; set; }
	public int ExternalId { get; set; }
	public string FullName { get; set; } = "";
	public int AvailableSeats { get; set; }
}
