using DataManagementService.Models;

namespace DataManagementService.Data.Repositories;

public interface IAirplaneVariantRepository
{
	void SaveChanges();
	IEnumerable<AirplaneVariant> GetAllAirplaneVariants(bool includeManufacturers = false);
	IEnumerable<AirplaneVariant> GetAirplaneVariantsInService(bool includeManufacturers = false);
	AirplaneVariant? GetAirplaneVariantById(int airplaneVariantId, bool includeManufacturer = false);
	bool AirplaneVariantExists(int airplaneId);
	void CreateAirplaneVariant(AirplaneVariant airplaneVariant);
}
