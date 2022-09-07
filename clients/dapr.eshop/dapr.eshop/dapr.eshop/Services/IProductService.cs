using dapr.eshop.Data;

namespace dapr.eshop.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetProducts();
    }
}
