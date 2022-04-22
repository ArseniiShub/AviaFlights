using DataManagementService.Models;

namespace DataManagementService.Data.Repositories;

public interface IAirportRepository
{
	void SaveChanges();
	Airport? GetAirportById(int airportId);
	IEnumerable<Airport> GetAllAirports();
	bool AirportExists(int airportId);
	void CreateAirport(Airport airport);
}
