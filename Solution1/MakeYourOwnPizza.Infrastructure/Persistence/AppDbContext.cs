using Microsoft.EntityFrameworkCore;
using MakeYourOwnPizza.Domain.Entities;
using MakeYourOwnPizza.Infrastructure.Persistence.Configurations;

namespace MakeYourOwnPizza.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<User> User { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }
        public DbSet<Pizza> Pizza { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<OrderIngredient> OrderIngredient { get; set; }
        public DbSet<OrderStage> OrderStage { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<EmailVerification> EmailVerification { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
