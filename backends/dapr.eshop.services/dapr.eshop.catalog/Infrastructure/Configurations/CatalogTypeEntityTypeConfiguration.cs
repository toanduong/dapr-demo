using dapr.eshop.catalog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dapr.eshop.catalog.Infrastructure.Configurations
{
    public class CatalogTypeEntityTypeConfiguration : IEntityTypeConfiguration<CatalogType>
    {
        public void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToContainer("CatalogType");

            builder.Property(type => type.Id)
                .IsRequired();

            builder.Property(type => type.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasData(
                new CatalogType(1, "Cap"),
                new CatalogType(2, "Mug"),
                new CatalogType(3, "Pin"),
                new CatalogType(4, "Sticker"),
                new CatalogType(5, "T-Shirt"));
        }
    }
}
