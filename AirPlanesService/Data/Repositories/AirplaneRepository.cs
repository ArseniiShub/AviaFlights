using Microsoft.EntityFrameworkCore;

namespace AirplanesService.Data.Repositories;

public class AirplaneRepository : IAirplaneRepository
{
	private readonly AirplanesDbContext _context;

	public AirplaneRepository(AirplanesDbContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public void SaveChanges()
	{
		_context.SaveChanges();
	}

	public Airplane? GetAirplaneById(int id)
	{
		return _context.Airplanes.FirstOrDefault(x => x.Id == id);
	}

	public IEnumerable<Airplane> GetAllAirplanes(bool includeManufacturers = false)
	{
		return includeManufacturers
			? _context.Airplanes.Include(a => a.Manufacturer).ToList()
			: _context.Airplanes.ToList();
	}

	public IEnumerable<Airplane> GetAirplanesInService()
	{
		return _context.Airplanes.Where(x => x.Status == Status.InService);
	}

	public bool AirplaneExists(int airplaneId)
	{
		return _context.Airplanes.Any(x => x.Id == airplaneId);
	}

	public void CreateAirplane(Airplane airplane)
	{
		ArgumentNullException.ThrowIfNull(airplane);

		_context.Airplanes.Add(airplane);
	}
}
