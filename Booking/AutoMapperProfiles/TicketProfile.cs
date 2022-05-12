using AutoMapper;
using Booking.Dtos;
using Booking.Models;

namespace Booking.AutoMapperProfiles;

// ReSharper disable once UnusedType.Global
public class TicketProfile : Profile
{
	public TicketProfile()
	{
		CreateMap<TicketCreateDto, Ticket>();
	}
}
