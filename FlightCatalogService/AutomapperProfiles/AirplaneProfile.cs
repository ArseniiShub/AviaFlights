using AutoMapper;
using FlightCatalogService.Dtos;
using FlightCatalogService.Models;

namespace FlightCatalogService.AutomapperProfiles;

// ReSharper disable once UnusedType.Global
public class AirplaneProfile : Profile
{
	public AirplaneProfile()
	{
		CreateMap<AirplanePublishDto, Airplane>()
			.ForMember(a => a.Id, opt => opt.Ignore())
			.ForMember(a => a.ExternalId, opt => opt.MapFrom(dto => dto.Id))
			.ForMember(a => a.FullName,
				opt => opt.MapFrom(dto => $"{dto.ManufacturerName} {dto.ModelName} {dto.SerialNumber}"));
	}
}
