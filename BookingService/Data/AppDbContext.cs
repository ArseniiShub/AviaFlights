using BookingService.Dtos;
using BookingService.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingService.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
	{
	}

	public DbSet<Ticket> Tickets { get; set; } = null!;
}
