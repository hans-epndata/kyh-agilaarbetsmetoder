using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers;

public interface IProductsController
{
    Task<IActionResult> Create([FromBody] ProductRegistrationForm form);
    Task<IActionResult> Get(string articleNumber);
    Task<IActionResult> GetAll();
}



[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductService productService) : ControllerBase, IProductsController
{
    private readonly IProductService _productService = productService;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProductRegistrationForm form)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var exists = await _productService.ProductExistsAsync(form.ArticleNumber);
            if (exists)
            {
                return Conflict();
            }
            else
            {
                var result = await _productService.CreateProductAsync(form);
                return Created("", result);
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return Problem();
    }

    [HttpGet("{articleNumber}")]
    public async Task<IActionResult> Get(string articleNumber)
    {

        try
        {
            if (string.IsNullOrWhiteSpace(articleNumber))
                return BadRequest();

            var result = await _productService.GetProductByArticleNumberAsync(articleNumber);
            if (result == null)
                return NotFound();

            return Ok(result);
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
            return Ok(result);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return Problem();

    }
}
