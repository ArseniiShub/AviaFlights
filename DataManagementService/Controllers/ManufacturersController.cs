using AutoMapper;
using DataManagementService.Data.Repositories;
using DataManagementService.Dtos;
using DataManagementService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers;

[Route("api/m/[controller]")]
[ApiController]
public class ManufacturersController : ControllerBase
{
	private readonly IManufacturerRepository _manufacturerRepository;
	private readonly ICountryRepository _countryRepository;
	private readonly IMapper _mapper;

	public ManufacturersController(IManufacturerRepository manufacturerRepository, ICountryRepository countryRepository,
		IMapper mapper)
	{
		_manufacturerRepository = manufacturerRepository;
		_countryRepository = countryRepository;
		_mapper = mapper;
	}

	[HttpGet]
	public ActionResult<IEnumerable<ManufacturerReadDto>> GetAllManufacturers()
	{
		var manufacturers = _manufacturerRepository.GetAllManufacturers();
		return Ok(_mapper.Map<IEnumerable<ManufacturerReadDto>>(manufacturers));
	}

	[HttpGet("{id:int}")]
	public ActionResult<IEnumerable<ManufacturerReadDto>> GetManufacturerById(int id)
	{
		var manufacturer = _manufacturerRepository.GetManufacturerById(id, true);

		if(manufacturer == null)
		{
			return NotFound();
		}

		return Ok(_mapper.Map<ManufacturerReadDto>(manufacturer));
	}

	[HttpPost]
	public ActionResult<AirplaneVariantReadDto> CreateManufacturer(ManufacturerCreateDto manufacturerCreateDto)
	{
		if(!_countryRepository.CountryExists(manufacturerCreateDto.CountryId))
		{
			return NotFound($"Could not find country with id {manufacturerCreateDto.CountryId}");
		}

		var manufacturer = _mapper.Map<Manufacturer>(manufacturerCreateDto);
		_manufacturerRepository.CreateManufacturer(manufacturer);
		_manufacturerRepository.SaveChanges();

		var manufacturerReadDto = _mapper.Map<ManufacturerReadDto>(manufacturer);
		return CreatedAtAction(nameof(GetManufacturerById), new { manufacturerReadDto.Id }, manufacturerReadDto);
	}
}
