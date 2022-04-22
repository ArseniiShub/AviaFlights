using AutoMapper;
using DataManagementService.AsyncDataServices;
using DataManagementService.Data.Repositories;
using DataManagementService.Dtos;
using DataManagementService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers;

[Route("api/m/[controller]")]
[ApiController]
public class AirplaneVariantsController : ControllerBase
{
	private readonly IAirplaneVariantRepository _airplaneVariantRepository;
	private readonly IManufacturerRepository _manufacturerRepository;
	private readonly IMapper _mapper;

	public AirplaneVariantsController(IAirplaneVariantRepository airplaneVariantRepository,
		IManufacturerRepository manufacturerRepository, IMapper mapper)
	{
		_airplaneVariantRepository = airplaneVariantRepository;
		_manufacturerRepository = manufacturerRepository;
		_mapper = mapper;
	}

	[HttpGet]
	public ActionResult<IEnumerable<AirplaneVariantReadDto>> GetAllAirplaneVariants()
	{
		var airplaneVariants = _airplaneVariantRepository.GetAllAirplaneVariants(true);
		return Ok(_mapper.Map<IEnumerable<AirplaneVariantReadDto>>(airplaneVariants));
	}

	[HttpGet("service")]
	public ActionResult<IEnumerable<AirplaneVariantReadDto>> GetAirplaneVariantsInService()
	{
		var airplaneVariants = _airplaneVariantRepository.GetAirplaneVariantsInService(true);
		return Ok(_mapper.Map<IEnumerable<AirplaneVariantReadDto>>(airplaneVariants));
	}

	[HttpGet("{id:int}")]
	public ActionResult<AirplaneVariantReadDto> GetAirplaneVariantById(int id)
	{
		var airplaneVariant = _airplaneVariantRepository.GetAirplaneVariantById(id, true);

		if(airplaneVariant == null)
		{
			return NotFound();
		}

		return Ok(_mapper.Map<AirplaneVariantReadDto>(airplaneVariant));
	}

	[HttpPost]
	public ActionResult<AirplaneVariantReadDto> CreateAirplaneVariant(AirplaneVariantCreateDto airplaneVariantCreateDto)
	{
		if(!_manufacturerRepository.ManufacturerExists(airplaneVariantCreateDto.ManufacturerId))
		{
			return NotFound($"Could not find manufacturer with id {airplaneVariantCreateDto.ManufacturerId}");
		}

		var airplaneVariant = _mapper.Map<AirplaneVariant>(airplaneVariantCreateDto);
		_airplaneVariantRepository.CreateAirplaneVariant(airplaneVariant);
		_airplaneVariantRepository.SaveChanges();

		var airplaneVariantReadDto = _mapper.Map<AirplaneVariantReadDto>(airplaneVariant);
		return CreatedAtAction(nameof(GetAirplaneVariantById), new {airplaneVariant.Id}, airplaneVariantReadDto);
	}
}
