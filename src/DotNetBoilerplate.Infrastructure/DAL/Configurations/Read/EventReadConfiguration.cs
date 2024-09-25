using System.Diagnostics.Eventing.Reader;
using DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read;

internal sealed class EventReadConfiguration : IEntityTypeConfiguration<EventReadModel>
{
    public void Configure(EntityTypeBuilder<EventReadModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.ToTable("Events");

        builder.HasOne(x => x.Organizer)
            .WithMany(x => x.OrganizedEvents)
            .HasForeignKey(x => x.OrganizerId);
        
    }
}