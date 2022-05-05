using AutoMapper;
using BookingService.Dtos;
using FlightCatalogService.Protos;

namespace BookingService.SyncDataServices.Grpc;

public class FlightDataClient : IFlightDataClient
{
	private readonly FlightService.FlightServiceClient _client;
	private readonly ILogger<FlightDataClient> _logger;
	private readonly IMapper _mapper;

	public FlightDataClient(FlightService.FlightServiceClient client, ILogger<FlightDataClient> logger, IMapper mapper)
	{
		_client = client;
		_logger = logger;
		_mapper = mapper;
	}

	public FlightDto GetFlight(int id)
	{
		_logger.LogInformation("Requesting flight with id: {Id}...", id);
		var reply = _client.GetFlight(new FlightRequest { Id = id });
		_logger.LogInformation("Successfully got flight");
		var flight = _mapper.Map<FlightDto>(reply);
		return flight;
	}
}
