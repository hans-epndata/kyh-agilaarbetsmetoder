using Manero.Domain.Interfaces;
using Manero.Domain.Models;
using System.Linq.Expressions;

namespace Manero.Domain.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    private readonly IProductRepository _repository = repository;

    public async Task<bool> AddProductAsync(IProduct product)
    {
        if (!await _repository.ExistsAsync(x => x.Name == product.Name))
        {
            ProductEntity entity = new ProductEntity()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };

            var result = await _repository.AddAsync(entity);
            return result;
        }

        return false;
    }

    public async Task<bool> DeleteProductAsync(Expression<Func<IProduct, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<IProduct>> GetAllProductsAsync()
    {
        var result = await _repository.GetAllAsync();
        
        List<IProduct> products = []; 
        foreach(var item in result)
        {
            products.Add(new Product
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price
            });
        }

        return products;
    }

    public async Task<IProduct> GetProductAsync(Expression<Func<IProduct, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateProductAsync(IProduct product)
    {
        throw new NotImplementedException();
    }
}
