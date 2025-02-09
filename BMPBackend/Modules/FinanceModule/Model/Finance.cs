using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BMPBackend.Modules.UserModule.User;

namespace BMPBackend.Modules.FinanceModule.Model
{
    public enum FinanceType
    {
        Expense,
        Revenue,
        Refund
    }

    public class Finance
    {
        public int Id { get; set; }
        public FinanceType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public class FinanceConfiguration : IEntityTypeConfiguration<Finance>
    {
        public void Configure(EntityTypeBuilder<Finance> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.HasIndex(x => x.UserId);

            builder.Property(x => x.Type)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Date)
                .IsRequired();

            builder.Property<string>(x => x.Description)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasOne(r => r.User)
                .WithMany(x => x.Finances)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
