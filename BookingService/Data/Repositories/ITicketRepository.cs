using BookingService.Models;

namespace BookingService.Data.Repositories;

public interface ITicketRepository
{
	bool SaveChanges();
	Ticket? GetTicketById(int id);
	void CreateTicket(Ticket ticket);
}
