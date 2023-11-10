using System.Diagnostics;
using WebApi.Helpers.Repositories;
using WebApi.Models;
using WebApi.Models.Products;

namespace WebApi.Helpers.Services;

public class ProductService
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ServiceResponse> CreateProductAsync(ProductCreateRequest request)
    {
        try
        {
            if (! await _productRepository.ExistsAsync(x => x.ArticleNumber == request.ArticleNumber))
            {
                Product product = await _productRepository.CreateAsync(request);
                if (product != null)
                {
                    return new ServiceResponse
                    {
                        Status = Enums.ResponseStatusCode.CREATED,
                        Message = "Product was created successfully",
                        Result = product
                    };
                }
            }
            else
                return new ServiceResponse
                {
                    Status = Enums.ResponseStatusCode.EXISTS,
                    Message = "A product with the same article number already exists",
                    Result = null
                };

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        
        return new ServiceResponse
        {
            Status = Enums.ResponseStatusCode.ERROR,
            Message = "Something went wrong while creating the product.",
            Result = null
        };
    }

    public async Task<ServiceResponse> GetProductsAsync()
    {
        try
        {
            IEnumerable<Product> products = (await _productRepository.GetAsync()).Select(productEntity => (Product)productEntity);

            return new ServiceResponse
            {
                Status = Enums.ResponseStatusCode.OK,
                Result = products
            };

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return new ServiceResponse
        {
            Status = Enums.ResponseStatusCode.ERROR,
            Message = "Something went wrong while creating the product.",
            Result = null
        };
    }
}
