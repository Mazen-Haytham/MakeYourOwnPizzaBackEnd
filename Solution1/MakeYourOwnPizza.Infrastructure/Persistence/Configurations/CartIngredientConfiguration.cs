using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Configurations
{
    public class CartIngredientConfiguration : IEntityTypeConfiguration<CartIngredient>
    {
        public void Configure(EntityTypeBuilder<CartIngredient> builder)
        {
            builder.HasKey(ci => ci.Id);

            builder.HasOne(ci => ci.CartItem)
                .WithMany(c => c.Ingredients)
                .HasForeignKey(ci => ci.CartItemId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ci => ci.Ingredients)
                .WithMany()
                .HasForeignKey(ci => ci.IngredientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
