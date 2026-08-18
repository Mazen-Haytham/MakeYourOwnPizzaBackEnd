using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.firstName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.lastName).IsRequired().HasMaxLength(50);
            builder.Property(u => u.email).IsRequired().HasMaxLength(100);
            builder.Property(u => u.password).IsRequired().HasMaxLength(100);
            builder.Property(u => u.phone).IsRequired().HasMaxLength(15);
            builder.Property(u => u.role).IsRequired().HasConversion<string>().HasMaxLength(20);
            builder.Property(u => u.refreshToken).HasMaxLength(200);
            builder.HasIndex(u => u.email).IsUnique();
            builder.HasMany(u => u.orders)
                   .WithOne(o => o.user)
                   .HasForeignKey(o => o.userId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.EmailVerifications)
                   .WithOne(ev => ev.User)
                   .HasForeignKey(ev => ev.UserId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
