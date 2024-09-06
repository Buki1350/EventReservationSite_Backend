using DotNetBoilerplate.Shared.Abstractions.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Write;

internal sealed class OutboxMessageWriteConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(x => x.Id);
    }
}