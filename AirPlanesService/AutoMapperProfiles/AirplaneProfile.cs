using System.Diagnostics.CodeAnalysis;
using AutoMapper;

namespace AirplanesService.AutoMapperProfiles;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class AirplaneProfile : Profile
{
	public AirplaneProfile()
	{
		CreateMap<Airplane, AirplaneReadDto>();
		CreateMap<AirplaneCreateDto, Airplane>();
	}
}
