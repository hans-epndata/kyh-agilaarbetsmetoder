using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RESTFUL_WebApi.Models;
using RESTFUL_WebApi.Services;

namespace RESTFUL_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _productService.GetProductsFromList();
            return SendStatusCodes(result);
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            var result = _productService.AddProductToList(product);
            return SendStatusCodes(result);
        }



        private IActionResult SendStatusCodes(ServiceResult result)
        {

            switch (result.Status)
            {
                case Models.ServiceCode.OK: return Ok(result);
                case Models.ServiceCode.NOTFOUND: return NotFound(result);
                case Models.ServiceCode.EXISTS: return Conflict(result);
                case Models.ServiceCode.ADDED: return Created("", result);
                default: return Ok();
            }
        }
    }
}
