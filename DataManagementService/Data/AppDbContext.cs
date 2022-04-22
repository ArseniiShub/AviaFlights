using DataManagementService.Extensions;
using DataManagementService.Models;
using Microsoft.EntityFrameworkCore;

namespace DataManagementService.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<AirplaneVariant> AirplaneVariants { get; set; } = null!;
	public DbSet<Airplane> Airplanes { get; set; } = null!;
	public DbSet<Manufacturer> Manufacturers { get; set; } = null!;
	public DbSet<Airport> Airports { get; set; } = null!;
	public DbSet<Country> Countries { get; set; } = null!;
	public DbSet<FlightRoute> FlightRoutes { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.SetOnDeleteRestrict();
	}
}
