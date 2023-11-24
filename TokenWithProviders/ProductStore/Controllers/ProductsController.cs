using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductStore.Models;
using ProductStore.Services;

namespace ProductStore.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductsController(ProductService productService) : ControllerBase
{
    private readonly ProductService _productService = productService;

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateRequest form)
    {
        if (ModelState.IsValid)
        {
            await _productService.CreateProductAsync(form);
            return Ok();
        }

        return BadRequest();
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

}
