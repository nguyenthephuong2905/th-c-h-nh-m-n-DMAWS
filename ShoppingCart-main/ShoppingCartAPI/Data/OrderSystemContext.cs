using Microsoft.EntityFrameworkCore;
using ShoppingCartAPI.Models;

namespace ShoppingCartAPI.Data
{
    public class OrderSystemContext : DbContext
    {
        public OrderSystemContext(DbContextOptions<OrderSystemContext> options)
            : base(options)
        {
        }

        public DbSet<OrderTbl> OrderTbl { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure OrderTbl
            modelBuilder.Entity<OrderTbl>(entity =>
            {
                entity.HasKey(e => e.OrderId);
                entity.Property(e => e.OrderId).ValueGeneratedOnAdd();
                entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
                
                // Indexes for better performance
                entity.HasIndex(e => e.ItemCode);
                entity.HasIndex(e => e.CreatedDate);
            });

            // Seed sample data
            modelBuilder.Entity<OrderTbl>().HasData(
                new OrderTbl
                {
                    OrderId = 1,
                    ItemCode = "ITEM001",
                    ItemName = "Laptop Dell XPS 13",
                    ItemQty = 1,
                    OrderDelivery = DateTime.Now.AddDays(3),
                    OrderAddress = "123 Nguyen Hue Street, District 1, Ho Chi Minh City",
                    PhoneNumber = "0901234567",
                    CreatedDate = DateTime.Now
                },
                new OrderTbl
                {
                    OrderId = 2,
                    ItemCode = "ITEM002",
                    ItemName = "iPhone 15 Pro Max",
                    ItemQty = 2,
                    OrderDelivery = DateTime.Now.AddDays(2),
                    OrderAddress = "456 Le Loi Street, District 3, Ho Chi Minh City",
                    PhoneNumber = "0907654321",
                    CreatedDate = DateTime.Now
                }
            );
        }
    }
}

