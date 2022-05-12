using Management.Models;

namespace Management.Data.Repositories;

public class CountryRepository : ICountryRepository
{
	private readonly AppDbContext _context;

	public CountryRepository(AppDbContext context)
	{
		_context = context;
	}

	public void SaveChanges()
	{
		_context.SaveChanges();
	}

	public Country? GetCountryById(int countryId)
	{
		return _context.Countries.FirstOrDefault(x => x.Id == countryId);
	}

	public IEnumerable<Country> GetAllCountries()
	{
		return _context.Countries.ToList();
	}

	public bool CountryExists(int countryId)
	{
		return _context.Countries.Any(x => x.Id == countryId);
	}

	public void CreateCountry(Country country)
	{
		ArgumentNullException.ThrowIfNull(country);

		_context.Countries.Add(country);
	}
}
