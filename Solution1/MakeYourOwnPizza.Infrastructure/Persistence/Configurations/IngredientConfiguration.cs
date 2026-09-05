using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Configurations
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredients>
    {
        public void Configure(EntityTypeBuilder<Ingredients> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.name).IsRequired().HasMaxLength(50);
            builder.Property(i=>i.colorHex).IsRequired().HasMaxLength(7);   
            builder.Property(i => i.price).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(i => i.isAvailable).IsRequired().HasDefaultValue(true);
            builder.HasMany(i => i.orderIngredients)
                   .WithOne(oi => oi.Ingredient)
                   .HasForeignKey(oi => oi.ingredientId);
        }
    }
}
