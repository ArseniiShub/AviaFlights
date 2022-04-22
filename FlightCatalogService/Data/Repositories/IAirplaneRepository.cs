using FlightCatalogService.Models;

namespace FlightCatalogService.Data.Repositories;

public interface IAirplaneRepository
{
	IEnumerable<Airplane> GetAllAirplanes();
	void CreateAirplane(Airplane airplane);
	bool SaveChanges();
}
