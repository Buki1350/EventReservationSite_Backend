using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read;

internal sealed class ReservationReadConfiguration : IEntityTypeConfiguration<ReservationReadModel>
{
    public void Configure(EntityTypeBuilder<ReservationReadModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Reservation");
        
        builder.HasOne(x => x.Event)
            .WithMany(x => x.Reservations)
            .HasForeignKey(x => x.EventId);
    }
}