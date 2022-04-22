using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using DataManagementService.Dtos;
using DataManagementService.Models;

namespace DataManagementService.AutoMapperProfiles;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class AirplaneVariantProfile : Profile
{
	public AirplaneVariantProfile()
	{
		CreateMap<AirplaneVariant, AirplaneVariantReadDto>();
		CreateMap<AirplaneVariantCreateDto, AirplaneVariant>();
	}
}
