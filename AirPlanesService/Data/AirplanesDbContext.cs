using Microsoft.EntityFrameworkCore;

namespace AirplanesService.Data;

public class AirplanesDbContext : DbContext
{
	public AirplanesDbContext(DbContextOptions<AirplanesDbContext> options) : base(options)
	{
	}

	public DbSet<Airplane> Airplanes { get; set; } = null!;
	public DbSet<Manufacturer> Manufacturers { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.Entity<Airplane>().HasKey(a => a.Id);
		builder.Entity<Airplane>().HasOne(a => a.Manufacturer)
			.WithMany(m => m.Airplanes);

		builder.Entity<Manufacturer>().HasKey(m => m.Id);
		builder.Entity<Manufacturer>().HasMany(m => m.Airplanes)
			.WithOne(a => a.Manufacturer);
	}
}
