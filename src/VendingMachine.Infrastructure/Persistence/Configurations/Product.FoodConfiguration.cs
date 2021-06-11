using VendingMachine.Application.Common.Extensions;
using VendingMachine.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace VendingMachine.Infrastructure.Persistence.Configurations
{
    public class FoodConfiguration : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.ToTable(nameof(Food).ToPlural(), "Product");

            builder.HasQueryFilter(m => EF.Property<bool>(m, "IsDeleted") == false);

            builder.Property(t => t.Title)
                .HasMaxLength(200)
                .IsRequired();
            builder.Property(t => t.Price)
                .HasPrecision(6, 2)
                .IsRequired();

        }
    }
}
