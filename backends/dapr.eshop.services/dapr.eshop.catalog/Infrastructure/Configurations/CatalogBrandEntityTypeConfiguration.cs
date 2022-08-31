using dapr.eshop.catalog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dapr.eshop.catalog.Infrastructure.Configurations
{
    public class CatalogBrandEntityTypeConfiguration : IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToContainer("CatalogBrand");

            builder.Property(brand => brand.Id)
                .IsRequired();

            builder.Property(brand => brand.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new CatalogBrand(1, ".NET"),
                new CatalogBrand(2, "Dapr"),
                new CatalogBrand(3, "Other"));
        }
    }
}
