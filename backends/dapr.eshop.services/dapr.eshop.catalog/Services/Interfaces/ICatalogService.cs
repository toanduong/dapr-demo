using dapr.eshop.catalog.Models;

namespace dapr.eshop.catalog.Services.Interfaces
{
    public interface ICatalogService
    {
        IEnumerable<CatalogItem> GetProducts();
    }
}
