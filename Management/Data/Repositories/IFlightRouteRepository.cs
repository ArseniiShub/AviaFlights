using Management.Models;

namespace Management.Data.Repositories;

public interface IFlightRouteRepository
{
	void SaveChanges();
	FlightRoute? GetFlightRouteById(int flightRouteId);
	IEnumerable<FlightRoute> GetAllFlightRoutes();
	void CreateFlightRoute(FlightRoute flightRoute);
}
