namespace AirplanesService.Data.Repositories;

public class ManufacturerRepository : IManufacturerRepository
{
	private readonly AirplanesDbContext _context;

	public ManufacturerRepository(AirplanesDbContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	public void SaveChanges()
	{
		_context.SaveChanges();
	}

	public Manufacturer? GetManufacturerById(int manufacturerId)
	{
		return _context.Manufacturers.FirstOrDefault(x => x.Id == manufacturerId);
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
