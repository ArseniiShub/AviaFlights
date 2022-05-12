using Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Management.Data.Repositories;

public class AirplaneVariantRepository : IAirplaneVariantRepository
{
	private readonly AppDbContext _context;

	public AirplaneVariantRepository(AppDbContext context)
	{
		_context = context;
	}

	public void SaveChanges()
	{
		_context.SaveChanges();
	}

	public IEnumerable<AirplaneVariant> GetAllAirplaneVariants(bool includeManufacturers = false)
	{
		return includeManufacturers
			? _context.AirplaneVariants.Include(a => a.Manufacturer).ToList()
			: _context.AirplaneVariants.ToList();
	}

	public IEnumerable<AirplaneVariant> GetAirplaneVariantsInService(bool includeManufacturers = false)
	{
		var query = _context.AirplaneVariants
			.Where(x => x.Status == Status.InService);

		return includeManufacturers
			? query.Include(x => x.Manufacturer)
			: query;
	}

	public AirplaneVariant? GetAirplaneVariantById(int airplaneVariantId, bool includeManufacturer = false)
	{
		return includeManufacturer
			? _context.AirplaneVariants
				.Include(x => x.Manufacturer)
				.FirstOrDefault(x => x.Id == airplaneVariantId)
			: _context.AirplaneVariants.FirstOrDefault(x => x.Id == airplaneVariantId);
	}

	public bool AirplaneVariantExists(int airplaneId)
	{
		return _context.AirplaneVariants.Any(x => x.Id == airplaneId);
	}

	public void CreateAirplaneVariant(AirplaneVariant airplaneVariant)
	{
		ArgumentNullException.ThrowIfNull(airplaneVariant);

		_context.AirplaneVariants.Add(airplaneVariant);
	}
}
