using dapr.eshop.catalog.Infrastructure;
using dapr.eshop.catalog.Models;
using dapr.eshop.catalog.Services.Interfaces;

namespace dapr.eshop.catalog.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly CatalogDbContext _context;

        public CatalogService(CatalogDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CatalogItem> GetProducts()
        {
            return _context.CatalogItems.ToList();
        }
    }
}
