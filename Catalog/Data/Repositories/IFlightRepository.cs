using Catalog.Models;

namespace Catalog.Data.Repositories;

public interface IFlightRepository
{
	IEnumerable<Flight> GetFlightsOnDay(DateOnly date, bool includeAirplane = false, bool includeFlightRoute = false);
	void CreateFlight(Flight flight);
	bool SaveChanges();
	Flight? GetFlightById(int id, bool includeAirplane = false, bool includeFlightRoute = false);
}
