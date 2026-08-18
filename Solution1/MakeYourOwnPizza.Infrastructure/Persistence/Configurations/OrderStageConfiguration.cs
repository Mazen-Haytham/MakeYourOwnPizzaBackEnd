using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MakeYourOwnPizza.Domain.Entities;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Configurations
{
    public class OrderStageConfiguration : IEntityTypeConfiguration<OrderStage>
    {
        public void Configure(EntityTypeBuilder<OrderStage> builder)
        {
            builder.HasKey(os => os.Id);
            builder.Property(os => os.orderId).IsRequired();
            builder.Property(os => os.stageType).IsRequired().HasMaxLength(50);
            builder.Property(os => os.createdAt).IsRequired();
            builder.HasIndex(os => new { os.orderId, os.createdAt, os.Id }).IsDescending(false, true, true);
            builder.HasOne(os => os.order)
                .WithMany(o => o.orderStages)
                .HasForeignKey(os => os.orderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
