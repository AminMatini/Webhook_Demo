using AirLineSendAgent.Entities;
using Microsoft.EntityFrameworkCore;

namespace AirLineSendAgent;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)	
	{

	}

	public DbSet<WebhookSubscription> WebhookSubscriptions { get; set; } = null!;
}

