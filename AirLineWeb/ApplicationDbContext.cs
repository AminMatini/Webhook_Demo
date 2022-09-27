using AirLineWeb.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirLineWeb;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}

	public DbSet<WebhookSubscribtion> WebhookSubscribtions { get; set; } = null!;
	public DbSet<FlightDetail> FlightDetails { get; set; } = null!;

}

