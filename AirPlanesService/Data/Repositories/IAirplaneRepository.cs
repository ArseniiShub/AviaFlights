namespace AirplanesService.Data.Repositories;

public interface IAirplaneRepository
{
	void SaveChanges();
	Airplane? GetAirplaneById(int id);
	IEnumerable<Airplane> GetAllAirplanes(bool includeManufacturers = false);
	IEnumerable<Airplane> GetAirplanesInService();
	bool AirplaneExists(int airplaneId);
	void CreateAirplane(Airplane airplane);
}
