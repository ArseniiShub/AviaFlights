namespace BookingService.Models;

public class Ticket
{
	public int Id { get; set; }
	public int FlightId { get; set; }
	public string PassengerName { get; set; } = "";
}
