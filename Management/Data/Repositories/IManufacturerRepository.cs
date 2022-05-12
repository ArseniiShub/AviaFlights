using Management.Models;

namespace Management.Data.Repositories;

public interface IManufacturerRepository
{
	void SaveChanges();
	Manufacturer? GetManufacturerById(int manufacturerId, bool includeCountry);
	IEnumerable<Manufacturer> GetAllManufacturers();
	bool ManufacturerExists(int manufacturerId);
	void CreateManufacturer(Manufacturer manufacturer);
}
