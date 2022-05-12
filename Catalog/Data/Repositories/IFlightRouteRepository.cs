using Catalog.Models;

namespace Catalog.Data.Repositories;

public interface IFlightRouteRepository
{
	IEnumerable<FlightRoute> GetAllFlightRoutes();
}
