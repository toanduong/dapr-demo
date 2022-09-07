using dapr.eshop.Data;
using Newtonsoft.Json;

namespace dapr.eshop.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProducts()
        {
            var products = new List<ProductViewModel>();
            var response = await _httpClient.GetAsync("api/products");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<IEnumerable<ProductViewModel>>(result).ToList();
            }

            return products;
        }
    }
}
