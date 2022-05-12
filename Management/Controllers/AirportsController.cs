using AutoMapper;
using Management.Data.Repositories;
using Management.Dtos;
using Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers;

[Route("api/m/[controller]")]
[ApiController]
public class AirportsController : ControllerBase
{
	private readonly IAirportRepository _airportRepository;
	private readonly ICountryRepository _countryRepository;
	private readonly IMapper _mapper;

	public AirportsController(IAirportRepository airportRepository, ICountryRepository countryRepository,
		IMapper mapper)
	{
		_airportRepository = airportRepository;
		_countryRepository = countryRepository;
		_mapper = mapper;
	}

	[HttpGet]
	public ActionResult<IEnumerable<AirportReadDto>> GetAllAirports()
	{
		var airports = _airportRepository.GetAllAirports();
		return Ok(_mapper.Map<IEnumerable<AirportReadDto>>(airports));
	}

	[HttpGet("{id:int}")]
	public ActionResult<IEnumerable<AirportReadDto>> GetAirportById(int id)
	{
		var airport = _airportRepository.GetAirportById(id);

		if(airport == null)
		{
			return NotFound();
		}

		return Ok(_mapper.Map<AirportReadDto>(airport));
	}

	[HttpPost]
	public ActionResult<AirportReadDto> CreateAirport(AirportCreateDto airportCreateDto)
	{
		if(!_countryRepository.CountryExists(airportCreateDto.CountryId))
		{
			return NotFound($"Could not find country with id {airportCreateDto.CountryId}");
		}

		var airport = _mapper.Map<Airport>(airportCreateDto);
		_airportRepository.CreateAirport(airport);
		_airportRepository.SaveChanges();

		var airportReadDto = _mapper.Map<AirportReadDto>(airport);
		return CreatedAtAction(nameof(GetAirportById), new { airportReadDto.Id }, airportReadDto);
	}
}
