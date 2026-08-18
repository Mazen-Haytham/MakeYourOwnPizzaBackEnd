using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Configurations
{
    public class OrderIngredientConfiguration : IEntityTypeConfiguration<OrderIngredient>
    {
        public void Configure(EntityTypeBuilder<OrderIngredient> builder)
        {
            builder.HasKey(oi => oi.Id);
            builder.Property(oi => oi.orderItemId).IsRequired();
            builder.Property(oi => oi.ingredientId).IsRequired();
            builder.Property(oi => oi.quantity).IsRequired();
            builder.HasOne(oi => oi.orderItem)
                .WithMany(o => o.orderIngredients)
                .HasForeignKey(oi => oi.orderItemId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(oi => oi.Ingredient)
                .WithMany()
                .HasForeignKey(oi => oi.ingredientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
