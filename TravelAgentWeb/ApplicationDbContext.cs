using Microsoft.EntityFrameworkCore;
using TravelAgentWeb.Entities;

namespace TravelAgentWeb;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}

	public DbSet<WebhookSecret> SubscriptionSecrets { get; set; } = null!;
}