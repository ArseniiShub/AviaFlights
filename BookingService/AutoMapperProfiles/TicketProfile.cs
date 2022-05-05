using AutoMapper;
using BookingService.Dtos;
using BookingService.Models;

namespace BookingService.AutoMapperProfiles;

// ReSharper disable once UnusedType.Global
public class TicketProfile : Profile
{
	public TicketProfile()
	{
		CreateMap<TicketCreateDto, Ticket>();
	}
}
