using DataManagementService.Dtos;
using DataManagementService.Models;
using Microsoft.EntityFrameworkCore;

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

	public Airplane? GetAirplaneById(int airplaneId, bool includeVariant = false)
	{
		return includeVariant
			? _context.Airplanes.Include(x => x.Variant).FirstOrDefault(x => x.Id == airplaneId)
			: _context.Airplanes.FirstOrDefault(x => x.Id == airplaneId);
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

	public AirplanePublishDto? GetAirplanePublishDto(int airplaneId)
	{
		return _context.Airplanes
			.Join(
				_context.AirplaneVariants,
				a => a.VariantId,
				av => av.Id,
				(a, av) => new
				{
					a.Id,
					a.TotalFlights,
					a.AvailableSeats,
					a.SerialNumber,
					av.Model,
					av.ManufacturerId
				}
			)
			.Join(_context.Manufacturers,
				x => x.ManufacturerId,
				m => m.Id,
				(x, m) => new AirplanePublishDto
				{
					Id = x.Id,
					AvailableSeats = x.AvailableSeats,
					TotalFlights = x.TotalFlights,
					ManufacturerName = m.Name,
					SerialNumber = x.SerialNumber,
					ModelName = x.Model
				})
			.FirstOrDefault(a => a.Id == airplaneId);
	}
}
