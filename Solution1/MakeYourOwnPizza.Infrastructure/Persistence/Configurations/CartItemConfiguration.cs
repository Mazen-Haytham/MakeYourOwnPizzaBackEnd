using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MakeYourOwnPizza.Domain.Entities;
namespace MakeYourOwnPizza.Infrastructure.Persistence.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.CartId).IsRequired();
            builder.Property(ci => ci.PizzaId).IsRequired();
            builder.Property(ci => ci.quantity).IsRequired();
            builder.HasIndex(ci => ci.CartId);

            builder.HasOne(ci => ci.Cart)
                .WithMany(c => c.Items)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ci => ci.Ingredients)
                .WithOne(cii => cii.CartItem)
                .HasForeignKey(cii => cii.CartItemId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(ci => ci.Pizza)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.PizzaId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
