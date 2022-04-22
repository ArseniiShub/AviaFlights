using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using DataManagementService.Dtos;
using DataManagementService.Models;

namespace DataManagementService.AutoMapperProfiles;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class FlightRouteProfile : Profile
{
	public FlightRouteProfile()
	{
		CreateMap<FlightRoute, FlightRouteReadDto>()
			.ForMember(dto => dto.ToAirportId, opt => opt.MapFrom(r => r.ToId))
			.ForMember(dto => dto.FromAirportId, opt => opt.MapFrom(r => r.FromId));

		CreateMap<FlightRouteCreateDto, FlightRoute>()
			.ForMember(r => r.ToId, opt => opt.MapFrom(dto => dto.ToAirportId))
			.ForMember(r => r.FromId, opt => opt.MapFrom(dto => dto.FromAirportId));
	}
}
