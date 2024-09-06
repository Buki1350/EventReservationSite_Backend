using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read;
using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Contexts;

internal sealed class DotNetBoilerplateReadDbContext(DbContextOptions<DotNetBoilerplateReadDbContext> options)
    : DbContext(options)
{
    public DbSet<UserReadModel> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dotNetBoilerplate");
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserReadConfiguration());
    }
}