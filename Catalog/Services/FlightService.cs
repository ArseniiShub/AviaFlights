using AutoMapper;
using Catalog.Data.Repositories;
using Catalog.Protos;
using Grpc.Core;

namespace Catalog.Services;

public class FlightService : Protos.FlightService.FlightServiceBase
{
	private readonly IFlightRepository _flightRepository;
	private readonly IMapper _mapper;
	private readonly ILogger<FlightService> _logger;

	public FlightService(IFlightRepository flightRepository, IMapper mapper, ILogger<FlightService> logger)
	{
		_flightRepository = flightRepository;
		_mapper = mapper;
		_logger = logger;
	}

	public override Task<FlightReply> GetFlight(FlightRequest request, ServerCallContext context)
	{
		_logger.LogInformation("Incoming call");
		if(request.Id < 1)
		{
			_logger.LogWarning("Invalid Flight Id received: {FlightId}", request.Id);
			throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid argument"),
				"Id must be greater than 1");
		}

		var flight = _flightRepository.GetFlightById(request.Id, true, true);

		if(flight is null)
		{
			throw new RpcException(new Status(StatusCode.NotFound, "Not found"));
		}

		return Task.FromResult(_mapper.Map<FlightReply>(flight));
	}
}
