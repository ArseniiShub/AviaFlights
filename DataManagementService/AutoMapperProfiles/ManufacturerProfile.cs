using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using DataManagementService.Dtos;
using DataManagementService.Models;

namespace DataManagementService.AutoMapperProfiles;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class ManufacturerProfile : Profile
{
	public ManufacturerProfile()
	{
		CreateMap<Manufacturer, ManufacturerReadDto>();
		CreateMap<ManufacturerCreateDto, Manufacturer>();
	}
}
