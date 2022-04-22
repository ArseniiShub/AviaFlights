using DataManagementService.Models;

namespace DataManagementService.Data.Repositories;

public interface IAirplaneRepository
{
	void SaveChanges();
	Airplane? GetAirplaneById(int airplaneId);
	IEnumerable<Airplane> GetAllAirplanes();
	void CreateAirplane(Airplane airplane);
}
