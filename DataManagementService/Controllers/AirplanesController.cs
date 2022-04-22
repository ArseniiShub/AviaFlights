using AutoMapper;
using DataManagementService.AsyncDataServices;
using DataManagementService.Data.Repositories;
using DataManagementService.Dtos;
using DataManagementService.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataManagementService.Controllers;

[Route("api/m/[controller]")]
[ApiController]
public class AirplanesController : ControllerBase
{
	private readonly IAirplaneRepository _airplaneRepository;
	private readonly IMessageBusClient _messageBusClient;
	private readonly IAirplaneVariantRepository _airplaneVariantRepository;
	private readonly IMapper _mapper;
	private readonly ILogger<AirplanesController> _logger;

	public AirplanesController(IAirplaneRepository airplaneRepository, IMessageBusClient messageBusClient,
		IAirplaneVariantRepository airplaneVariantRepository, IMapper mapper, ILogger<AirplanesController> logger)
	{
		_airplaneRepository = airplaneRepository;
		_messageBusClient = messageBusClient;
		_airplaneVariantRepository = airplaneVariantRepository;
		_mapper = mapper;
		_logger = logger;
	}

	[HttpGet]
	public ActionResult<IEnumerable<AirplaneReadDto>> GetAllAirplanes()
	{
		var airplanes = _airplaneRepository.GetAllAirplanes();
		return Ok(_mapper.Map<IEnumerable<AirplaneReadDto>>(airplanes));
	}

	[HttpGet("{id:int}")]
	public ActionResult<IEnumerable<AirplaneReadDto>> GetAirplaneById(int id)
	{
		var airplane = _airplaneRepository.GetAirplaneById(id);

		if(airplane == null)
		{
			return NotFound();
		}

		return Ok(_mapper.Map<AirplaneReadDto>(airplane));
	}

	[HttpPost]
	public ActionResult<Airplane> CreateAirplane(AirplaneCreateDto airplaneCreateDto)
	{
		if(!_airplaneVariantRepository.AirplaneVariantExists(airplaneCreateDto.VariantId))
		{
			return NotFound($"Could not find airplane variant with id {airplaneCreateDto.VariantId}");
		}

		var airplane = _mapper.Map<Airplane>(airplaneCreateDto);
		_airplaneRepository.CreateAirplane(airplane);
		_airplaneRepository.SaveChanges();

		try
		{
			_messageBusClient.PublishAirplane(_mapper.Map<AirplanePublishDto>(airplane));
		}
		catch(Exception e)
		{
			_logger.LogError(e, "Could not publish new airplane (Id: {Id})", airplane.Id);
		}

		var airplaneReadDto = _mapper.Map<AirplaneReadDto>(airplane);
		return CreatedAtAction(nameof(GetAirplaneById), new { airplane.Id }, airplaneReadDto);
	}
}
