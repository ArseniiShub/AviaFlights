using FlightCatalogService.Models;

namespace FlightCatalogService.Data.Repositories;

public class FlightRouteRepository : IFlightRouteRepository
{
	private readonly AppDbContext _context;

	public FlightRouteRepository(AppDbContext context)
	{
		_context = context;
	}

	public IEnumerable<FlightRoute> GetAllFlightRoutes()
	{
		return _context.FlightRoutes.ToList();
	}
}
