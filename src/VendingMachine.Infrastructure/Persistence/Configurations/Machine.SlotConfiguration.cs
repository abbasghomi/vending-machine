using VendingMachine.Application.Common.Extensions;
using VendingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VendingMachine.Infrastructure.Persistence.Configurations
{
    public class SlotConfiguration : IEntityTypeConfiguration<Slot>
    {
        public void Configure(EntityTypeBuilder<Slot> builder)
        {
            builder.ToTable(nameof(Slot).ToPlural(), "Machine");

            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

            builder.Property(t => t.SlotNumber)
                .HasMaxLength(10)
                .IsRequired();

        }
    }
}
