using AutoMapper;
using FlightCatalogService.Data.Repositories;
using FlightCatalogService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FlightCatalogService.Controllers;

[Route("api/c/[controller]")]
[ApiController]
public class FlightsController : ControllerBase
{
	private readonly IFlightRepository _flightRepository;
	private readonly IMapper _mapper;

	public FlightsController(IFlightRepository flightRepository, IMapper mapper, ILogger<FlightsController> logger)
	{
		_flightRepository = flightRepository;
		_mapper = mapper;
	}

	[HttpGet("{date}")]
	public ActionResult<IEnumerable<FlightReadDto>> GetFlightsOnDate(string date)
	{
		if(!DateOnly.TryParseExact(date, "dd-MM-yyyy", out var parsedDate))
		{
			return BadRequest("Invalid date format");
		}

		var flights = _flightRepository.GetFlightsOnDay(parsedDate, true, true);

		return Ok(_mapper.Map<IEnumerable<FlightReadDto>>(flights));
	}
}
