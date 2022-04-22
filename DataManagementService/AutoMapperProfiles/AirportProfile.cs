using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using DataManagementService.Dtos;
using DataManagementService.Models;

namespace DataManagementService.AutoMapperProfiles;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class AirportProfile : Profile
{
	public AirportProfile()
	{
		CreateMap<Airport, AirportReadDto>();
		CreateMap<AirportCreateDto, Airport>();
	}
}
