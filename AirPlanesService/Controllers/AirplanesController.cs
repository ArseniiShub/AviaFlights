using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirplanesService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AirplanesController : ControllerBase
{
	private readonly IAirplaneRepository _airplaneRepository;
	private readonly IManufacturerRepository _manufacturerRepository;
	private readonly IMapper _mapper;

	public AirplanesController(IAirplaneRepository airplaneRepository, IManufacturerRepository manufacturerRepository, IMapper mapper)
	{
		_airplaneRepository = airplaneRepository ?? throw new ArgumentNullException(nameof(airplaneRepository));
		_manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
		_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
	}

	[HttpGet]
	public ActionResult<IEnumerable<AirplaneReadDto>> GetAllAirplanes()
	{
		var airplanes = _airplaneRepository.GetAllAirplanes(true);
		return Ok(_mapper.Map<IEnumerable<AirplaneReadDto>>(airplanes));
	}

	[HttpGet("inservice")]
	public ActionResult<IEnumerable<AirplaneReadDto>> GetAirplanesInService()
	{
		var airplanes = _airplaneRepository.GetAirplanesInService();
		return Ok(_mapper.Map<IEnumerable<AirplaneReadDto>>(airplanes));
	}

	[HttpGet("{id}")]
	public ActionResult<IEnumerable<AirplaneReadDto>> GetAirplaneById(int id)
	{
		var airplanes = _airplaneRepository.GetAirplanesInService();
		return Ok(_mapper.Map<IEnumerable<AirplaneReadDto>>(airplanes));
	}

	[HttpPost]
	public ActionResult<AirplaneReadDto> CreateNewAirplane(AirplaneCreateDto airplaneCreateDto)
	{
		if(!_manufacturerRepository.ManufacturerExists(airplaneCreateDto.ManufacturerId))
		{
			return NotFound($"Could not find manufacturer with id {airplaneCreateDto.ManufacturerId}");
		}

		var airplane = _mapper.Map<Airplane>(airplaneCreateDto);
		_airplaneRepository.CreateAirplane(airplane);
		_airplaneRepository.SaveChanges();

		var airplaneReadDto = _mapper.Map<AirplaneReadDto>(airplane);
		return CreatedAtAction(nameof(GetAirplaneById), new { airplane.Id }, airplaneReadDto);
	}
}
