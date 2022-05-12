using Catalog.Models;
using Catalog.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<Flight> Flights { get; set; } = null!;
	public DbSet<Airplane> Airplanes { get; set; } = null!;
	public DbSet<FlightRoute> FlightRoutes { get; set; } = null!;
	public DbSet<Airport> Airports { get; set; } = null!;
	public DbSet<Country> Countries { get; set; } = null!;
	
	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.SetOnDeleteRestrict();
	}
}
