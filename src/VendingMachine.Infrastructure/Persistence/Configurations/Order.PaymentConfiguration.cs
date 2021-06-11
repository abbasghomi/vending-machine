using VendingMachine.Application.Common.Extensions;
using VendingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VendingMachine.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable(nameof(Payment).ToPlural(), "Order");

            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

            builder.Property(t => t.Amount)
                .HasPrecision(6, 2)
                .IsRequired();


            builder.HasOne(d => d.Invoice)
               .WithOne(p => p.Payment)
               .HasForeignKey<Invoice>(d => d.PaymentId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Payment_Invoice_PaymentId");

        }
    }
}
