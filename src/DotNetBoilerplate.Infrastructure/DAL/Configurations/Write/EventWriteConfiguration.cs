using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Core.Reservations;
using DotNetBoilerplate.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Write;

internal sealed class EventWriteConfiguration : IEntityTypeConfiguration<Event>
{
    //rezerwacje na Owns Many
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id) 
            .HasConversion(e => e.Value, e => new EventId(e));
        
        builder.Property(e => e.OrganizerId).HasConversion(e => e.Value, e => new UserId(e));
        
        builder.Property(e => e.Title).HasConversion(e => e.Value, e => new EventTitle(e));
        
        builder.Property(e => e.Description).HasConversion(e => e.Value, e => new EventDescription(e));
        
        builder.Property(e => e.StartDate).HasConversion(e => e.Value, e => new EventStartDate());
        
        builder.Property(e => e.EndDate).HasConversion(e => e.Value, e => new EventEndDate(e));
        
        builder.Property(e => e.Location).HasConversion(e => e.Value, e => new EventLocation(e));
        
        builder.Property(e => e.MaxNumberOfReservations).HasConversion(e => e.Value, e => new EventMaxNumberOfReservations(e));

        builder.OwnsMany(e => e.Reservations, reservationBuilder =>
        {
            reservationBuilder.HasKey(r => r.Id);
            reservationBuilder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new ReservationId(x));
            reservationBuilder.Property(x => x.EventId)
                .HasConversion(x => x.Value, x => new EventId(x));
            reservationBuilder.Property(x => x.UserId)
                .HasConversion(x => x.Value, x => new UserId(x));
            reservationBuilder.Property(x => x.CreatedAt);
            reservationBuilder.Property(x => x.Paid)
                .HasConversion(x => x.Value, x => new ReservationPaid(x));
            reservationBuilder.Property(x => x.Active);
            
            reservationBuilder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
        });

        builder.HasOne<User>().WithMany().HasForeignKey(e => e.OrganizerId);

    }
}