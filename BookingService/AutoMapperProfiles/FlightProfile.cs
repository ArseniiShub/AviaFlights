using AutoMapper;
using BookingService.Dtos;
using FlightCatalogService.Protos;

namespace BookingService.AutoMapperProfiles;

// ReSharper disable once UnusedType.Global
public class FlightProfile : Profile
{
	public FlightProfile()
	{
		CreateMap<FlightReply, FlightDto>()
			.ForMember(f => f.Departure, reply => reply.MapFrom(f => DateTime.Parse(f.DepartureDate)))
			.ForMember(f => f.Arrival, reply => reply.MapFrom(f => DateTime.Parse(f.ArrivalDate)));
	}
}
