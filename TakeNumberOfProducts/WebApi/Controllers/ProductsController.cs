using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ProductService productService) : ControllerBase
    {
        private readonly ProductService _productService = productService;


        [HttpGet]
        public async Task<IActionResult> GetProducts() 
        { 
            var products = await _productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{take}")]
        public async Task<IActionResult> GetProducts(int take)
        {
            var products = await _productService.GetProductsAsync(take);
            return Ok(products);
        }
    }
}
