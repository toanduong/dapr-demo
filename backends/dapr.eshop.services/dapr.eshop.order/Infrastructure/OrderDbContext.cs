using dapr.eshop.order.Models;
using Microsoft.EntityFrameworkCore;

namespace dapr.eshop.order.Infrastructure
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("orders");

            modelBuilder.Entity<Order>().OwnsMany(s => s.OrderTrackings);
            modelBuilder.Entity<Order>().OwnsMany(s => s.OrderItems);
            modelBuilder.Entity<Order>().OwnsOne(s => s.OrderSummary);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
