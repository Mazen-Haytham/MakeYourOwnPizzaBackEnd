using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Configurations
{
    public class OrderItemsConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.orderId).IsRequired();
            builder.Property(oi => oi.pizzaId).IsRequired();
            builder.Property(oi => oi.quantity).IsRequired();
            builder.Property(oi => oi.price).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasOne(oi => oi.order)
                .WithMany(o => o.orderItems)
                .HasForeignKey(oi => oi.orderId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(oi => oi.pizza)
                .WithMany()
                .HasForeignKey(oi => oi.pizzaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
