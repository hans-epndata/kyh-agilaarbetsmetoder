using System.Diagnostics;
using WebApi.Models;
using WebApi.Models.Entities;
using WebApi.Repositories;

namespace WebApi.Services;

public interface IProductService
{
    Task<Product> CreateProductAsync(ProductRegistrationForm form);
    Task<Product> GetProductByArticleNumberAsync(string articleNumber);
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> UpdateProductAsync(string articleNumber, ProductUpdateForm form);

    Task<bool> DeleteProductAsync(string articleNumber);
    Task<bool> ProductExistsAsync(string articleNumber);
}

public class ProductService(IProductRepository productRepository) : IProductService
{
    private IProductRepository _productRepository = productRepository;



    public async Task<Product> CreateProductAsync(ProductRegistrationForm form)
    {
        try
        {
            if (! await _productRepository.ExistsAsync(x => x.ArticleNumber == form.ArticleNumber))
            {
                var result = await _productRepository.CreateAsync(new ProductEntity
                {
                    ArticleNumber = form.ArticleNumber,
                    Name = form.Name,
                    Description = form.Description
                });

                if (result != null)
                {
                    return new Product
                    {
                        ArticleNumber = result.ArticleNumber,
                        Name = result.Name,
                        Description = result.Description
                    };
                }
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> DeleteProductAsync(string articleNumber)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;

    }

    public async Task<Product> GetProductByArticleNumberAsync(string articleNumber)
    {
        try
        {
            var result = await _productRepository.GetAsync(x => x.ArticleNumber == articleNumber);
            if (result != null)
            {
                return new Product
                {
                    ArticleNumber = result.ArticleNumber,
                    Name = result.Name,
                    Description = result.Description
                };
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;

    }

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        try
        {
            var result = await _productRepository.GetAllAsync();
            return result.Select(x => new Product
            {
                ArticleNumber = x.ArticleNumber,
                Name = x.Name,
                Description = x.Description
            });

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;

    }

    public async Task<bool> ProductExistsAsync(string articleNumber)
    {
        try
        {
            var result = await _productRepository.ExistsAsync(x => x.ArticleNumber == articleNumber);
            return result;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public async Task<Product> UpdateProductAsync(string articleNumber, ProductUpdateForm form)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
        
    }
}
