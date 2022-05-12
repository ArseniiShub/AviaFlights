using System.Net.Mime;
using AutoMapper;
using Management.AsyncDataServices;
using Management.Data.Repositories;
using Management.Dtos;
using Management.Enums;
using Management.Models;
using Microsoft.AspNetCore.Mvc;

namespace Management.Controllers;

[Route("api/m/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
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

	/// <summary>
	/// Returns all airplanes
	/// </summary>
	/// <returns></returns>
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public ActionResult<IEnumerable<AirplaneReadDto>> GetAllAirplanes()
	{
		var airplanes = _airplaneRepository.GetAllAirplanes();
		return Ok(_mapper.Map<IEnumerable<AirplaneReadDto>>(airplanes));
	}

	/// <summary>
	/// Returns an airplane for given id
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
	[HttpGet("{id:int}")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public ActionResult<IEnumerable<AirplaneReadDto>> GetAirplaneById(int id)
	{
		var airplane = _airplaneRepository.GetAirplaneById(id);

		if(airplane == null)
		{
			return NotFound();
		}

		return Ok(_mapper.Map<AirplaneReadDto>(airplane));
	}

	/// <summary>
	/// Creates airplane using provided data
	/// </summary>
	/// <param name="airplaneCreateDto"></param>
	/// <returns></returns>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public ActionResult<AirplaneReadDto> CreateAirplane(AirplaneCreateDto airplaneCreateDto)
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
			var airplanePublishDto = _airplaneRepository.GetAirplanePublishDto(airplane.Id);
			if(airplanePublishDto == null)
			{
				throw new InvalidOperationException("Could not get AirplanePublishDto");
			}

			airplanePublishDto.EventType = EventType.AirplanePublished;
			_messageBusClient.PublishAirplane(airplanePublishDto);
		}
		catch(Exception e)
		{
			_logger.LogError(e, "Could not publish new airplane (Id: {Id})", airplane.Id);
		}

		var airplaneReadDto = _mapper.Map<AirplaneReadDto>(airplane);
		return CreatedAtAction(nameof(GetAirplaneById), new { airplane.Id }, airplaneReadDto);
	}
}
