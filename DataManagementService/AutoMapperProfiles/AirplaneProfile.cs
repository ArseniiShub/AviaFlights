using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using DataManagementService.Dtos;
using DataManagementService.Models;

namespace DataManagementService.AutoMapperProfiles;

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
