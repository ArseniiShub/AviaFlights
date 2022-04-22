using AutoMapper;
using FlightCatalogService.Dtos;
using FlightCatalogService.Models;

namespace FlightCatalogService.AutomapperProfiles;

public class AirplaneProfile : Profile
{
	public AirplaneProfile()
	{
		CreateMap<AirplanePublishDto, Airplane>()
			.ForMember(a => a.ExternalId, opt => opt.MapFrom(dto => dto.Id));
	}
}
