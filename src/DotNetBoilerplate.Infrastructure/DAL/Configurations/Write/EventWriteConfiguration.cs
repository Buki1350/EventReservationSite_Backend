using DotNetBoilerplate.Core.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Write;

internal sealed class EventWriteConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new EventId(x));
        
        builder.HasIndex(x => x.Title).IsUnique();
        builder.Property(x => x.Title)
            .HasConversion(x => x.Value, x => new EventTitle(x))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, x => new EventDescription(x))
            .HasMaxLength(2000); 

        builder.Property(x => x.Location)
            .HasConversion(x => x.Value, x => new EventLocation(x))
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.EventDate)
            .HasConversion(x => x.Value, x => new EventDate(x))
            .IsRequired();

        builder.Property(x => x.MaxNumberOfTickets)
            .HasConversion(x => x.Value, x => new MaxNumberOfTickets(x))
            .IsRequired();

        builder.Property(x => x.OrganizerId)
            .HasConversion(x => x.Value, x => new EventOrganizerId(x))
            .IsRequired();
        
    }
}