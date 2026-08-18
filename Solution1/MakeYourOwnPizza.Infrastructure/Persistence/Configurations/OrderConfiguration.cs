using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.userId).IsRequired();
            builder.Property(o => o.createdAt).IsRequired();
            builder.Property(o => o.totalPrice).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(o => o.paymentMethod).IsRequired().HasConversion<string>();
            builder.Property(o => o.estimatedDelivery).IsRequired();
            builder.Property(o => o.isActive).IsRequired();
            builder.HasIndex(o => new { o.userId, o.isActive });
            builder.HasMany(o => o.payments)
                .WithOne(p => p.order)
                .HasForeignKey(p => p.orderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(o => o.orderItems)
                .WithOne(oi => oi.order)
                .HasForeignKey(oi => oi.orderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(o => o.orderStages)
                .WithOne(os => os.order)
                .HasForeignKey(os => os.orderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(o => o.user)
                .WithMany(u => u.orders)
                .HasForeignKey(o => o.userId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
