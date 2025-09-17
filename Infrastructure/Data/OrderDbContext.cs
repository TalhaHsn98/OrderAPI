using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class OrdersDbContext : DbContext
    {
        public OrdersDbContext(DbContextOptions<OrdersDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderDetail> Order_Details => Set<OrderDetail>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Order>(e =>
            {
                e.ToTable("Order_History");     
                e.HasKey(x => x.Id);

                e.Property(x => x.CustomerId).HasMaxLength(64);
                e.Property(x => x.CustomerName).HasMaxLength(200);
                e.Property(x => x.PaymentName).HasMaxLength(100);
                e.Property(x => x.ShippingAddress).HasMaxLength(500);
                e.Property(x => x.ShippingMethod).HasMaxLength(100);
                e.Property(x => x.Order_Status).HasMaxLength(50);

                e.Property(x => x.BillAmount).HasColumnType("decimal(18,2)");
                e.Property(x => x.Order_Date).HasColumnType("datetime2");
            });

            b.Entity<OrderDetail>(e =>
            {
                e.ToTable("Order_Details");
                e.HasKey(x => x.Id);

                e.Property(x => x.Product_name).HasMaxLength(200);
                e.Property(x => x.Price).HasColumnType("decimal(18,2)");
                e.Property(x => x.Discount).HasColumnType("decimal(18,2)");

                e.HasOne(x => x.Order)
                 .WithMany(o => o.Order_Details)
                 .HasForeignKey(x => x.Order_Id)
                 .OnDelete(DeleteBehavior.Cascade);
            });


            b.Entity<Order>().HasData(
            new Order
            {
                Id = 1,
                Order_Date = new DateTime(2025, 9, 17),
                CustomerId = "CUST-1001",
                CustomerName = "Alice",
                PaymentMethodId = 1,
                PaymentName = "Credit Card",
                ShippingAddress = "123 Main St, Toledo, OH",
                ShippingMethod = "Ground",
                BillAmount = 115.00m,
                Order_Status = "Created"
            },
            new Order
            {
                Id = 2,
                Order_Date = new DateTime(2025, 9, 17),
                CustomerId = "CUST-1002",
                CustomerName = "Bob",
                PaymentMethodId = 2,
                PaymentName = "PayPal",
                ShippingAddress = "456 Oak St, Cleveland, OH",
                ShippingMethod = "Air",
                BillAmount = 220.00m,
                Order_Status = "Shipped"
            }
        );

            b.Entity<OrderDetail>().HasData(
                new OrderDetail
                {
                    Id = 1,
                    Order_Id = 1,   
                    Product_Id = 10,
                    Product_name = "Keyboard",
                    Qty = 1,
                    Price = 45.00m,
                    Discount = 0
                },
                new OrderDetail
                {
                    Id = 2,
                    Order_Id = 1,   
                    Product_Id = 11,
                    Product_name = "Mouse",
                    Qty = 2,
                    Price = 35.00m,
                    Discount = 5.00m
                },
                new OrderDetail
                {
                    Id = 3,
                    Order_Id = 2,   
                    Product_Id = 12,
                    Product_name = "Monitor",
                    Qty = 1,
                    Price = 220.00m,
                    Discount = 0
                }
            );
        }
    }
}
