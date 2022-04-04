namespace AirplanesService.Data.Repositories;

public interface IManufacturerRepository
{
	void SaveChanges();
	Manufacturer? GetManufacturerById(int manufacturerId);
	IEnumerable<Manufacturer> GetAllManufacturers();
	bool ManufacturerExists(int manufacturerId);
	void CreateManufacturer(Manufacturer manufacturer);
}
