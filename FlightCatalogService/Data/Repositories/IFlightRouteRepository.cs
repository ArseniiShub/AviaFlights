using FlightCatalogService.Models;

namespace FlightCatalogService.Data.Repositories;

public interface IFlightRouteRepository
{
	IEnumerable<FlightRoute> GetAllFlightRoutes();
}
