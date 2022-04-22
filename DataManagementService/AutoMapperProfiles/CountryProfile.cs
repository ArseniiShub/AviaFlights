using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using DataManagementService.Dtos;
using DataManagementService.Models;

namespace DataManagementService.AutoMapperProfiles;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class CountryProfile : Profile
{
	public CountryProfile()
	{
		CreateMap<Country, CountryReadDto>();
		CreateMap<CountryCreateDto, Country>();
	}
}
