namespace BookingService.Dtos;

public class FlightDto
{
	public int Id { get; set; }
	public DateTime Departure { get; set; }
	public DateTime Arrival { get; set; }
	public string FlightRouteName { get; set; } = "";
	public string AirplaneFullName { get; set; } = "";
}
