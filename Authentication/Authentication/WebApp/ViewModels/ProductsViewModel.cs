using WebApp.Models.Products;

namespace WebApp.ViewModels
{
    public class ProductsViewModel
    {
        public Product BestSeller { get; set; } = null!;
        public IEnumerable<Product> FeaturedProducts { get; set; } = null!;
        public IEnumerable<Product> ProductsForMen { get; set; } = null!;
        public IEnumerable<Product> ProductsForWomen { get; set; } = null!;
    }
}
