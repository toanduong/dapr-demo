using dapr.eshop.catalog.Infrastructure.Configurations;
using dapr.eshop.catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace dapr.eshop.catalog.Infrastructure
{
    public class CatalogDbContext : DbContext
    {
        public DbSet<CatalogBrand> CatalogBrands => Set<CatalogBrand>();
        public DbSet<CatalogItem> CatalogItems => Set<CatalogItem>();
        public DbSet<CatalogType> CatalogTypes => Set<CatalogType>();

        public CatalogDbContext(DbContextOptions<CatalogDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
            builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
        }
    }
}
