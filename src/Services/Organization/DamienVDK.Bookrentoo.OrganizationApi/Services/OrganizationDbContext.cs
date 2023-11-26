using Microsoft.EntityFrameworkCore;

namespace DamienVDK.Bookrentoo.OrganizationApi.Services;

public class OrganizationDbContext : DbContext
{
    public DbSet<Organization> Organizations { get; set; }
    public DbSet<Organization> Members { get; set; }

    public OrganizationDbContext(DbContextOptions<OrganizationDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
