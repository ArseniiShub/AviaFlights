using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using FlightCatalogService.Dtos;
using FlightCatalogService.Models;

namespace FlightCatalogService.AutomapperProfiles;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class FlightProfile : Profile
{
	public FlightProfile()
	{
		CreateMap<Flight, FlightReadDto>()
			.ForMember(dto => dto.AirplaneName, opt => opt.MapFrom(f => f.Airplane.SerialNumber))
			.ForMember(dto => dto.FlightRouteName, opt => opt.MapFrom(f => f.FlightRoute.Name));
	}
}
