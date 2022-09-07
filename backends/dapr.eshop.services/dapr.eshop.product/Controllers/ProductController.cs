using dapr.eshop.product.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace dapr.eshop.product.Controllers
{
    [Route("api/products")]
    [ApiController]
    [EnableCors]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<List<Product>> Get()
        {
            return await Product.GetAllAsync();
        }
    }
}
