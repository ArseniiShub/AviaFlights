﻿using System.ComponentModel.DataAnnotations;

namespace DataManagementService.Dtos;

public class AirportCreateDto
{
	[Required] public string Name { get; set; } = "";
	public string? City { get; set; }
	[Required] public double Latitude { get; set; }
	[Required] public double Longitude { get; set; }
	[Required] public int CountryId { get; set; }
}
