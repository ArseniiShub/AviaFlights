using Management.Dtos;
using Management.Models;

namespace Management.Data.Repositories;

public interface IAirplaneRepository
{
	void SaveChanges();
	Airplane? GetAirplaneById(int airplaneId, bool includeVariant = false);
	IEnumerable<Airplane> GetAllAirplanes();
	void CreateAirplane(Airplane airplane);

	AirplanePublishDto? GetAirplanePublishDto(int airplaneId);
}
