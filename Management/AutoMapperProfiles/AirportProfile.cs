using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Management.Dtos;
using Management.Models;

namespace Management.AutoMapperProfiles;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class AirportProfile : Profile
{
	public AirportProfile()
	{
		CreateMap<Airport, AirportReadDto>();
		CreateMap<AirportCreateDto, Airport>();
	}
}
