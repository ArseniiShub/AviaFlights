using System.Diagnostics.CodeAnalysis;
using AutoMapper;

namespace AirplanesService.AutoMapperProfiles;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class ManufacturerProfile : Profile
{
	public ManufacturerProfile()
	{
		CreateMap<Manufacturer, ManufacturerReadDto>();
		CreateMap<ManufacturerCreateDto, Manufacturer>();
	}
}
