using FlightCatalogService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightCatalogService.Data.Repositories;

public class FlightRepository : IFlightRepository
{
	private readonly AppDbContext _context;

	public FlightRepository(AppDbContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public IEnumerable<Flight> GetFlightsOnDay(DateOnly date, bool includeAirplane = false,
		bool includeFlightRoute = false)
	{
		var dateTime = date.ToDateTime(new TimeOnly());
		var query = _context.Flights.Where(f => f.Departure.Date == dateTime && f.AvailableSeats > 0);

		if(includeAirplane)
		{
			query.Include(f => f.Airplane);
		}

		if(includeFlightRoute)
		{
			query.Include(f => f.FlightRoute);
		}

		return query.ToList();
	}

	public void CreateFlight(Flight flight)
	{
		ArgumentNullException.ThrowIfNull(flight);

		_context.Flights.Add(flight);
	}

	public bool SaveChanges()
	{
		return _context.SaveChanges() > 0;
	}

	public Flight? GetFlightById(int id, bool includeAirplane = false, bool includeFlightRoute = false)
	{
		var query = _context.Flights.Where(f => f.Id == id);

		if(includeAirplane)
		{
			query = query.Include(f => f.Airplane);
		}

		if(includeFlightRoute)
		{
			query = query.Include(f => f.FlightRoute);
		}

		return query.FirstOrDefault();
	}
}
