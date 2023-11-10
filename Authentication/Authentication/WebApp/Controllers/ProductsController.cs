using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Helpers.Services;
using WebApp.Models.Products;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var viewModel = new ProductsViewModel();
                viewModel.ProductsForMen = (IEnumerable<Product>)(await _productService.GetProductsAsync()).Result!;
                viewModel.ProductsForWomen = (IEnumerable<Product>)(await _productService.GetProductsAsync()).Result!;

                return View(viewModel);
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return RedirectToAction("Index", "Error");

        }
    }
}
