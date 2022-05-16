using AutoMapper;
using Catalog.ConstantValues;
using Catalog.Data;
using Catalog.Data.Repositories;
using Catalog.Models;

namespace Catalog.Services;

public class FlightGenerator : IFlightGenerator
{
	private readonly IServiceScopeFactory _serviceScopeFactory;
	private readonly ILogger<FlightGenerator> _logger;
	private readonly int _maxFlightsPerDay;

	public FlightGenerator(IServiceScopeFactory serviceScopeFactory, IConfiguration configuration, ILogger<FlightGenerator> logger)
	{
		_serviceScopeFactory = serviceScopeFactory;
		_logger = logger;
		_maxFlightsPerDay = configuration.GetValue<int>(ConfigKeyPaths.MaxFlightsPerDay);
	}

	public void Generate(DateOnly start, DateOnly end)
	{
		using var scope = _serviceScopeFactory.CreateScope();
		using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
		var airplaneRepository = scope.ServiceProvider.GetRequiredService<IAirplaneRepository>();
		var flightRouteRepository = scope.ServiceProvider.GetRequiredService<IFlightRouteRepository>();
		var flightRepository = scope.ServiceProvider.GetRequiredService<IFlightRepository>();

		var availableAirplanes = airplaneRepository.GetAllAirplanes().ToList();
		var availableRoutes = flightRouteRepository.GetAllFlightRoutes().ToList();

		int flightCountToGenerate = _maxFlightsPerDay;

		if(availableAirplanes.Count < _maxFlightsPerDay)
		{
			flightCountToGenerate = availableAirplanes.Count;
		}

		if(flightCountToGenerate == 0 || !availableRoutes.Any())
		{
			_logger.LogWarning("Flights generation skipped");
			return;
		}

		for(var date = start; date < end; date = date.AddDays(1))
		{
			var flightsOnDay = flightRepository.GetFlightsOnDay(date).ToList();
			if(flightsOnDay.Any())
			{
				continue;
			}

			var generatedFlights =
				GenerateFlightsOnDay(availableAirplanes, availableRoutes, flightCountToGenerate, date).ToList();

			context.Flights.AddRange(generatedFlights);
			context.SaveChanges();
		}
	}

	private IEnumerable<Flight> GenerateFlightsOnDay(IList<Airplane> availableAirplanes, IList<FlightRoute> routes,
		int flightsCount, DateOnly date)
	{
		var rnd = new Random();
		var notUsedAirplanes = availableAirplanes.ToList();
		IList<Flight> result = new List<Flight>();

		for(var i = 0; i < flightsCount; i++)
		{
			var airplane = notUsedAirplanes[rnd.Next(0, notUsedAirplanes.Count)];
			var route = routes[rnd.Next(0, routes.Count)];

			var flight = new Flight
			{
				Airplane = airplane,
				AvailableSeats = airplane.AvailableSeats - rnd.Next(0, 6),
				FlightRoute = route,
				Departure = date.ToDateTime(new TimeOnly(2 + rnd.Next(7), 0, 0), DateTimeKind.Utc),
				Arrival = date.ToDateTime(new TimeOnly(10 + rnd.Next(11), 0, 0), DateTimeKind.Utc)
			};

			result.Add(flight);
			notUsedAirplanes.Remove(airplane);
		}

		return result;
	}
}
