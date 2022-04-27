using DataManagementService.Models;

namespace DataManagementService.Data;

public class TestDataCreator
{
	private readonly AppDbContext _context;

	public TestDataCreator(AppDbContext context)
	{
		_context = context;
	}

	public void FillData()
	{
		if(_context.Countries.Any())
		{
			return;
		}

		_context.Countries.AddRange(
			new Country { Name = "USA" },
			new Country { Name = "United Kingdom" },
			new Country { Name = "Russia" }
		);

		_context.Manufacturers.Add(new Manufacturer { CountryId = 1, Name = "Boeing" });

		_context.Airports.AddRange(
			new Airport
			{
				City = "Chicago", Name = "O'Hare", CountryId = 1, Latitude = 41.978611, Longitude = -87.904722
			},
			new Airport
			{
				City = "London", Name = "Heathrow", CountryId = 2, Latitude = 51.4775, Longitude = -0.461389
			}
		);

		_context.FlightRoutes.Add(new FlightRoute
		{
			FromId = 2,
			ToId = 1,
			Name = "Heathrow - O'Hare"
		});

		_context.AirplaneVariants.Add(new AirplaneVariant
		{
			ManufacturerId = 1,
			Status = Status.InService,
			Model = "737"
		});

		_context.Airplanes.Add(new Airplane
		{
			VariantId = 1,
			SerialNumber = "737-01",
			AvailableSeats = 30
		});

		_context.SaveChanges();
	}
}
