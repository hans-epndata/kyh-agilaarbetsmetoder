using Microsoft.EntityFrameworkCore;
using ProductStore.Contexts;
using ProductStore.Factories;
using ProductStore.Models;

namespace ProductStore.Services;

public class ProductService(DataContext context)
{
    private readonly DataContext _context = context;

    public async Task CreateProductAsync(ProductCreateRequest request)
    {
        var entity = ProductFactory.Create(request);

        _context.Products.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        var result = await _context.Products.Select(product => ProductFactory.Create(product)).ToListAsync();
        return result ?? null!;
    }
}
