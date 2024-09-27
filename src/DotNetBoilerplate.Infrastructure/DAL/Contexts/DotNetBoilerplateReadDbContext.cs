using System.Runtime.CompilerServices;
using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read;
using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;
using Microsoft.EntityFrameworkCore;



namespace DotNetBoilerplate.Infrastructure.DAL.Contexts;

internal sealed class DotNetBoilerplateReadDbContext(DbContextOptions<DotNetBoilerplateReadDbContext> options)
    : DbContext(options)
{
    public DbSet<UserReadModel> Users { get; set; }
    public DbSet<EventReadModel> Events { get; set; }
    
    public DbSet<ReservationReadModel> Reservations { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dotNetBoilerplate");
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserReadConfiguration());
        modelBuilder.ApplyConfiguration(new EventReadConfiguration());
        modelBuilder.ApplyConfiguration(new ReservationReadConfiguration());
    }
}