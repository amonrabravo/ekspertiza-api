using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EkspertizaWebApiData.Infrastructure;

public class AppDbContext : IdentityDbContext<User, Role, Guid>
{
	public AppDbContext(DbContextOptions options)
		: base(options)
	{

	}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder
            .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }

    public DbSet<City> Cities { get; set; }
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public DbSet<Service> Services { get; set; }

}