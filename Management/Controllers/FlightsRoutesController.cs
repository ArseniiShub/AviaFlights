using AutoMapper;
using Management.Data.Repositories;
using Management.Dtos;
using Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers;

[Route("api/m/[controller]")]
[ApiController]
public class FlightRoutesController : ControllerBase
{
	private readonly IFlightRouteRepository _flightRouteRepository;
	private readonly IAirportRepository _airportRepository;
	private readonly IMapper _mapper;

	public FlightRoutesController(IFlightRouteRepository flightRouteRepository, IAirportRepository airportRepository,
		IMapper mapper)
	{
		_flightRouteRepository = flightRouteRepository;
		_airportRepository = airportRepository;
		_mapper = mapper;
	}

	[HttpGet]
	public ActionResult<IEnumerable<FlightRouteReadDto>> GetAllFlightRoutes()
	{
		var flightRoutes = _flightRouteRepository.GetAllFlightRoutes();
		return Ok(_mapper.Map<IEnumerable<FlightRouteReadDto>>(flightRoutes));
	}

	[HttpGet("{id:int}")]
	public ActionResult<IEnumerable<FlightRouteReadDto>> GetFlightRouteById(int id)
	{
		var flightRoute = _flightRouteRepository.GetFlightRouteById(id);

		if(flightRoute == null)
		{
			return NotFound();
		}

		return Ok(_mapper.Map<FlightRouteReadDto>(flightRoute));
	}

	[HttpPost]
	public ActionResult<FlightRouteReadDto> CreateFlightRoute(FlightRouteCreateDto flightRouteCreateDto)
	{
		if(!_airportRepository.AirportExists(flightRouteCreateDto.FromAirportId))
		{
			return NotFound($"Could not find departure airport with id {flightRouteCreateDto.FromAirportId}");
		}

		if(!_airportRepository.AirportExists(flightRouteCreateDto.ToAirportId))
		{
			return NotFound($"Could not find arrival airport with id {flightRouteCreateDto.ToAirportId}");
		}

		var flightRoute = _mapper.Map<FlightRoute>(flightRouteCreateDto);
		_flightRouteRepository.CreateFlightRoute(flightRoute);
		_flightRouteRepository.SaveChanges();

		var flightRouteReadDto = _mapper.Map<FlightRouteReadDto>(flightRoute);
		return CreatedAtAction(nameof(GetFlightRouteById), new { flightRouteReadDto.Id }, flightRouteReadDto);
	}
}
