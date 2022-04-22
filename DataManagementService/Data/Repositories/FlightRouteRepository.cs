using DataManagementService.Models;

namespace DataManagementService.Data.Repositories;

public class FlightRouteRepository : IFlightRouteRepository
{
	private readonly AppDbContext _context;

	public FlightRouteRepository(AppDbContext context)
	{
		_context = context;
	}

	public void SaveChanges()
	{
		_context.SaveChanges();
	}

	public FlightRoute? GetFlightRouteById(int flightRouteId)
	{
		return _context.FlightRoutes.FirstOrDefault(x => x.Id == flightRouteId);
	}

	public IEnumerable<FlightRoute> GetAllFlightRoutes()
	{
		return _context.FlightRoutes.ToList();
	}

	public void CreateFlightRoute(FlightRoute flightRoute)
	{
		ArgumentNullException.ThrowIfNull(flightRoute);

		_context.FlightRoutes.Add(flightRoute);
	}
}
