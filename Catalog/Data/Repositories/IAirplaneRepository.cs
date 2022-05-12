using Catalog.Models;

namespace Catalog.Data.Repositories;

public interface IAirplaneRepository
{
	IEnumerable<Airplane> GetAllAirplanes();
	void CreateAirplane(Airplane airplane);
	bool SaveChanges();
}
