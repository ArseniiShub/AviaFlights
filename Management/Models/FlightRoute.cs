namespace Management.Models;

public class FlightRoute
{
	public int Id { get; set; }
	public string Name { get; set; } = "";

	public int FromId { get; init; }
	public Airport From { get; set; } = null!;

	public int ToId { get; init; }
	public Airport To { get; set; } = null!;
}
