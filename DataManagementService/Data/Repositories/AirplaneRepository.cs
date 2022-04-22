using DataManagementService.Models;

namespace DataManagementService.Data.Repositories;

public class AirplaneRepository : IAirplaneRepository
{
	private readonly AppDbContext _context;

	public AirplaneRepository(AppDbContext context)
	{
		_context = context;
	}

	public void SaveChanges()
	{
		_context.SaveChanges();
	}

	public Airplane? GetAirplaneById(int airplaneId)
	{
		return _context.Airplanes.FirstOrDefault(x => x.Id == airplaneId);
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
}
