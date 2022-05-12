using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Management.Dtos;
using Management.Models;

namespace Management.AutoMapperProfiles;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class ManufacturerProfile : Profile
{
	public ManufacturerProfile()
	{
		CreateMap<Manufacturer, ManufacturerReadDto>();
		CreateMap<ManufacturerCreateDto, Manufacturer>();
	}
}
