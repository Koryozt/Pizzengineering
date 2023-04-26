using Microsoft.EntityFrameworkCore;

namespace Pizzengineering.Persistence;

public sealed class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
	{

	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
	}
}
