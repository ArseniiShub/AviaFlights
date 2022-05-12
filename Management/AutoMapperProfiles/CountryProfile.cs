using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Management.Dtos;
using Management.Models;

namespace Management.AutoMapperProfiles;

[SuppressMessage("ReSharper", "UnusedType.Global")]
public class CountryProfile : Profile
{
	public CountryProfile()
	{
		CreateMap<Country, CountryReadDto>();
		CreateMap<CountryCreateDto, Country>();
	}
}
