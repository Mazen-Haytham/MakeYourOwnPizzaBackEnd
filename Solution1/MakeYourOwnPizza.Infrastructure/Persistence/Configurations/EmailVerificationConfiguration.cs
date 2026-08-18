using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Configurations
{
    public class EmailVerificationConfiguration : IEntityTypeConfiguration<EmailVerification>
    {
        public void Configure(EntityTypeBuilder<EmailVerification> builder)
        {
            builder.HasKey(ev => ev.Id);
            builder.Property(ev => ev.OtpHash).IsRequired();
            builder.Property(ev => ev.ExpiresAt).IsRequired();
            builder.Property(ev => ev.IsUsed).IsRequired();
            builder.Property(ev => ev.CreatedAt).IsRequired();
            builder.HasIndex(ev => ev.UserId);
            builder.HasOne(ev => ev.User)
                .WithMany(u => u.EmailVerifications)
                .HasForeignKey(ev => ev.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
