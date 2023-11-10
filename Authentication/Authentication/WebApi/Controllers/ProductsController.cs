using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.Filters;
using WebApi.Helpers.Services;
using WebApi.Models.Products;

namespace WebApi.Controllers
{
    [Route("api/products")]
    [ApiController]
    [UseApiKey]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var result = await _productService.CreateProductAsync(request);

                return result.Status switch
                {
                    Helpers.Enums.ResponseStatusCode.CREATED => Created("", result.Result),
                    Helpers.Enums.ResponseStatusCode.EXISTS => Conflict(result.Message),
                    _ => Problem(result.Message),
                };
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }      
            return Problem();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _productService.GetProductsAsync();
                return Ok(result.Result);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return Problem();
        }
    }
}
