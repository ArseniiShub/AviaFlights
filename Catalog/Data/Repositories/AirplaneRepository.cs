using Catalog.Models;

namespace Catalog.Data.Repositories;

public class AirplaneRepository : IAirplaneRepository
{
	private readonly AppDbContext _context;

	public AirplaneRepository(AppDbContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public IEnumerable<Airplane> GetAllAirplanes()
	{
		return _context.Airplanes.ToList();
	}

	public void CreateAirplane(Airplane airplane)
	{
		ArgumentNullException.ThrowIfNull(airplane);

		_context.Airplanes.Add(airplane);
	}

	public bool SaveChanges()
	{
		return _context.SaveChanges() > 0;
	}
}
