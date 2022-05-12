using Booking.Models;

namespace Booking.Data.Repositories;

public interface ITicketRepository
{
	bool SaveChanges();
	Ticket? GetTicketById(int id);
	void CreateTicket(Ticket ticket);
}
