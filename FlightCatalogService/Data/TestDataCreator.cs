using FlightCatalogService.Models;

namespace FlightCatalogService.Data;

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

		var countries = new[]
		{
			new Country { Name = "USA" },
			new Country { Name = "United Kingdom" },
			new Country { Name = "Russia" }
		};

		_context.Countries.AddRange(countries);

		var airports = new[]
		{
			new Airport
			{
				ExternalId = 1,
				City = "Chicago",
				Name = "O'Hare",
				Country = countries[0],
				Latitude = 41.978611,
				Longitude = -87.904722
			},
			new Airport
			{
				ExternalId = 2,
				City = "London",
				Name = "Heathrow",
				Country = countries[1],
				Latitude = 51.4775,
				Longitude = -0.461389
			}
		};

		_context.Airports.AddRange(airports);

		var airplanes = new[]
		{
			new Airplane
			{
				ExternalId = 1,
				AvailableSeats = 30,
				FullName = "Boeing 737 737-01"
			},
			new Airplane
			{
				ExternalId = 1,
				AvailableSeats = 20,
				FullName = "Boeing 737 737-02"
			}
		};

		_context.Airplanes.AddRange(airplanes);

		var flightRoutes = new[]
		{
			new FlightRoute
			{
				Name = "Heathrow - O'Hare",
				FromAirport = airports[1],
				ToAirport = airports[0],
				ExternalId = 1
			}
		};

		_context.FlightRoutes.AddRange(flightRoutes);

		_context.SaveChanges();
	}
}
