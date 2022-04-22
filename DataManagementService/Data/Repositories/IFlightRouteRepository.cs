using DataManagementService.Models;

namespace DataManagementService.Data.Repositories;

public interface IFlightRouteRepository
{
	void SaveChanges();
	FlightRoute? GetFlightRouteById(int flightRouteId);
	IEnumerable<FlightRoute> GetAllFlightRoutes();
	void CreateFlightRoute(FlightRoute flightRoute);
}
