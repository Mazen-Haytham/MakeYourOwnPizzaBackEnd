using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Configurations
{
    public class PizzaConfiguration : IEntityTypeConfiguration<Pizza>
    {
        public void Configure(EntityTypeBuilder<Pizza> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.price).IsRequired().HasColumnType("decimal(18,2)");
        }
    }
}
