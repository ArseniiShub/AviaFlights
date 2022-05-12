using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Management.Dtos;
using Management.Models;

namespace Management.AutoMapperProfiles;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class AirplaneProfile : Profile
{
	public AirplaneProfile()
	{
		CreateMap<Airplane, AirplaneReadDto>()
			.ForMember(dto => dto.AirplaneVariantId, opt => opt.MapFrom(a => a.VariantId));
		CreateMap<AirplaneCreateDto, Airplane>();
	}
}
