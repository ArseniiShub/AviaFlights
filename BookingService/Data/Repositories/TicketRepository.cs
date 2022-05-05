using BookingService.Models;

namespace BookingService.Data.Repositories;

public class TicketRepository : ITicketRepository
{
	private readonly AppDbContext _context;

	public TicketRepository(AppDbContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public bool SaveChanges()
	{
		return _context.SaveChanges() > 0;
	}

	public Ticket? GetTicketById(int id)
	{
		return _context.Tickets.FirstOrDefault(t => t.Id == id);
	}

	public void CreateTicket(Ticket ticket)
	{
		ArgumentNullException.ThrowIfNull(ticket);

		_context.Tickets.Add(ticket);
	}
}
