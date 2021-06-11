using VendingMachine.Application.Common.Extensions;
using VendingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VendingMachine.Infrastructure.Persistence.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable(nameof(Invoice).ToPlural(), "Order");

            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);


            builder.Property(t => t.ItemPrice)
                .HasPrecision(6, 2)
                .IsRequired();
            builder.Property(t => t.RefundAmount)
                .HasPrecision(6, 2)
                .IsRequired();

            builder.HasOne(d => d.Payment)
               .WithOne(p => p.Invoice)
               .HasForeignKey<Payment>(d => d.InvoiceId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Invoice_Payment_InvoiceId");

            builder.HasOne(d => d.RefundedInvoice)
               .WithOne()
               .IsRequired(false)
               .HasForeignKey<Invoice>(d => d.RefundedInvoiceId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_Invoice_Invoice_RefundedInvoiceId");

        }
    }
}
