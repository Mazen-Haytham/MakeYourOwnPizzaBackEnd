using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Zone).IsRequired();
            builder.Property(d => d.Status).IsRequired().HasConversion<string>();

            // 1-to-1 relationship with User
            builder.HasOne(d => d.User)
                .WithOne(u => u.Driver)
                .HasForeignKey<Driver>(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
