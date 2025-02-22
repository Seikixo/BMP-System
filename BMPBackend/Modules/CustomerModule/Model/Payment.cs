using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMPBackend.Modules.CustomerModule.Model
{
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Cancelled
    }

    public enum PaymentMethod
    {
        Cash,
        Card,
        BankTransfer
    }

    public class Payment
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }

    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.OrderId)
                .IsRequired();

            builder.HasIndex(x => x.OrderId);

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.PaymentStatus)
                .HasConversion<string>()
                .HasDefaultValue(PaymentStatus.Pending)
                .IsRequired();

            builder.Property(x => x.PaymentMethod)
                .HasConversion<string>()
                .HasDefaultValue(PaymentMethod.Cash)
                .IsRequired();

            builder.HasOne(r => r.Order)
                .WithMany(x => x.Payments)
                .HasForeignKey(r => r.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
