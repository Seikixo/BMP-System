using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMPBackend.Modules.UserModule.User;

namespace BMPBackend.Modules.CustomerModule.Model
{
    public enum OrderStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled

    }
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }

    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CustomerId)
                .IsRequired();

            builder.HasIndex(x => x.CustomerId);

            builder.Property(x => x.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.OrderStatus)
                .HasConversion<string>()
                .HasDefaultValue(OrderStatus.Pending)
                .IsRequired();

            builder.Property(x => x.OrderDate)
                .IsRequired();

            builder.HasOne(r => r.Customer)
                .WithMany(x => x.Orders)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
