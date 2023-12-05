using WebApi.Repositories;

namespace WebApi.Services;


public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _productRepository.GetAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsAsync(int take)
    {
        return await _productRepository.GetAsync(take);
    }


}
