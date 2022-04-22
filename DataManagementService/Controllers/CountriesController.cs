using AutoMapper;
using DataManagementService.Data.Repositories;
using DataManagementService.Dtos;
using DataManagementService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers;

[Route("api/m/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
	private readonly ICountryRepository _countryRepository;
	private readonly IMapper _mapper;

	public CountriesController(ICountryRepository countryRepository, IMapper mapper)
	{
		_countryRepository = countryRepository;
		_mapper = mapper;
	}

	[HttpGet]
	public ActionResult<IEnumerable<CountryReadDto>> GetAllCountries()
	{
		var countries = _countryRepository.GetAllCountries();
		return Ok(_mapper.Map<IEnumerable<CountryReadDto>>(countries));
	}

	[HttpGet("{id:int}")]
	public ActionResult<IEnumerable<CountryReadDto>> GetCountryById(int id)
	{
		var country = _countryRepository.GetCountryById(id);

		if(country == null)
		{
			return NotFound();
		}

		return Ok(_mapper.Map<CountryReadDto>(country));
	}

	[HttpPost]
	public ActionResult<CountryReadDto> CreateCountry(CountryCreateDto countryCreateDto)
	{
		var country = _mapper.Map<Country>(countryCreateDto);
		_countryRepository.CreateCountry(country);
		_countryRepository.SaveChanges();

		var countryReadDto = _mapper.Map<CountryReadDto>(country);
		return CreatedAtAction(nameof(GetCountryById), new {countryReadDto.Id}, countryReadDto);
	}
}
