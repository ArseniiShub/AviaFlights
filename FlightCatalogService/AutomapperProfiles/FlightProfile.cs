using System.Globalization;
using AutoMapper;
using FlightCatalogService.Dtos;
using FlightCatalogService.Models;
using FlightCatalogService.Protos;

namespace FlightCatalogService.AutomapperProfiles;

// ReSharper disable once UnusedType.Global
public class FlightProfile : Profile
{
	public FlightProfile()
	{
		CreateMap<Flight, FlightReadDto>()
			.ForMember(dto => dto.AirplaneFullName, opt => opt.MapFrom(f => f.Airplane.FullName))
			.ForMember(dto => dto.FlightRouteName, opt => opt.MapFrom(f => f.FlightRoute.Name));

		CreateMap<Flight, FlightReply>()
			.ForMember(reply => reply.FlightRouteName, opt => opt.MapFrom(f => f.FlightRoute.Name))
			.ForMember(reply => reply.AirplaneFullName, opt => opt.MapFrom(f => f.Airplane.FullName))
			.ForMember(reply => reply.DepartureDate,
				opt => opt.MapFrom(f => f.Departure.ToString(CultureInfo.InvariantCulture)))
			.ForMember(reply => reply.ArrivalDate,
				opt => opt.MapFrom(f => f.Arrival.ToString(CultureInfo.InvariantCulture)));
	}
}
