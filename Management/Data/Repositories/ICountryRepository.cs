using Management.Models;

namespace Management.Data.Repositories;

public interface ICountryRepository
{
	void SaveChanges();
	Country? GetCountryById(int countryId);
	IEnumerable<Country> GetAllCountries();
	bool CountryExists(int countryId);
	void CreateCountry(Country country);
}
