using Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Management.Data.Repositories;

public class ManufacturerRepository : IManufacturerRepository
{
	private readonly AppDbContext _context;

	public ManufacturerRepository(AppDbContext context)
	{
		_context = context;
	}

	public void SaveChanges()
	{
		_context.SaveChanges();
	}

	public Manufacturer? GetManufacturerById(int manufacturerId, bool includeCountry)
	{
		return includeCountry
			? _context.Manufacturers
				.Include(m => m.Country)
				.FirstOrDefault(m => m.Id == manufacturerId)
			: _context.Manufacturers.FirstOrDefault(x => x.Id == manufacturerId);
	}

	public IEnumerable<Manufacturer> GetAllManufacturers()
	{
		return _context.Manufacturers.ToList();
	}

	public bool ManufacturerExists(int manufacturerId)
	{
		return _context.Manufacturers.Any(x => x.Id == manufacturerId);
	}

	public void CreateManufacturer(Manufacturer manufacturer)
	{
		ArgumentNullException.ThrowIfNull(manufacturer);

		_context.Manufacturers.Add(manufacturer);
	}
}
