using Management.Models;

namespace Management.Data.Repositories;

public class AirportRepository : IAirportRepository
{
	private readonly AppDbContext _context;

	public AirportRepository(AppDbContext context)
	{
		_context = context;
	}

	public void SaveChanges()
	{
		_context.SaveChanges();
	}

	public Airport? GetAirportById(int airportId)
	{
		return _context.Airports.FirstOrDefault(x => x.Id == airportId);
	}

	public IEnumerable<Airport> GetAllAirports()
	{
		return _context.Airports.ToList();
	}

	public bool AirportExists(int airportId)
	{
		return _context.Airports.Any(x => x.Id == airportId);
	}

	public void CreateAirport(Airport airport)
	{
		ArgumentNullException.ThrowIfNull(airport);

		_context.Airports.Add(airport);
	}
}
