using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Management.Dtos;
using Management.Models;

namespace Management.AutoMapperProfiles;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class AirplaneVariantProfile : Profile
{
	public AirplaneVariantProfile()
	{
		CreateMap<AirplaneVariant, AirplaneVariantReadDto>();
		CreateMap<AirplaneVariantCreateDto, AirplaneVariant>();
	}
}
