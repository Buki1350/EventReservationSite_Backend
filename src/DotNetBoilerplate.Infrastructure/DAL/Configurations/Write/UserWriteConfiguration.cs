using DotNetBoilerplate.Core.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Write;

internal sealed class UserWriteConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion(x => x.Value, x => new UserId(x));

        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email).HasConversion(x => x.Value, x => new Email(x));

        builder.HasIndex(x => x.Username).IsUnique();
        builder.Property(x => x.Username).HasConversion(x => x.Value, x => new Username(x));

        builder.Property(x => x.Password)
            .HasConversion(x => x.Value, x => new Password(x))
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Role)
            .HasConversion(x => x.Value, x => new Role(x))
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.CreatedAt).IsRequired();

        builder.Property(x => x.AccountType)
            .HasConversion(x => x.Value, x => new AccountType(x))
            .IsRequired();
    }
}