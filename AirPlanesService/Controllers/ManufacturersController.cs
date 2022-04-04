using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AirplanesService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ManufacturersController : ControllerBase
{
	private readonly IManufacturerRepository _manufacturerRepository;
	private readonly IMapper _mapper;

	public ManufacturersController(IManufacturerRepository manufacturerRepository, IMapper mapper)
	{
		_manufacturerRepository = manufacturerRepository
		                          ?? throw new ArgumentNullException(nameof(manufacturerRepository));
		_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
	}

	[HttpGet]
	public ActionResult<IEnumerable<ManufacturerReadDto>> GetAllManufacturers()
	{
		var manufacturers = _manufacturerRepository.GetAllManufacturers();
		return Ok(_mapper.Map<IEnumerable<ManufacturerReadDto>>(manufacturers));
	}

	[HttpGet("{id}")]
	public ActionResult<IEnumerable<ManufacturerReadDto>> GetManufacturerById(int id)
	{
		var manufacturer = _manufacturerRepository.GetManufacturerById(id);
		return Ok(_mapper.Map<ManufacturerReadDto>(manufacturer));
	}

	[HttpPost]
	public ActionResult<AirplaneReadDto> CreateNewManufacturer(ManufacturerCreateDto manufacturerCreateDto)
	{
		var manufacturer = _mapper.Map<Manufacturer>(manufacturerCreateDto);
		_manufacturerRepository.CreateManufacturer(manufacturer);
		_manufacturerRepository.SaveChanges();

		var manufacturerReadDto = _mapper.Map<ManufacturerReadDto>(manufacturer);
		return CreatedAtAction(nameof(GetManufacturerById), new { manufacturerReadDto.Id }, manufacturerReadDto);
	}
}
