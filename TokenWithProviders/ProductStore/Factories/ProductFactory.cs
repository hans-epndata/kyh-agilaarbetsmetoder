using ProductStore.Models;

namespace ProductStore.Factories;

public static class ProductFactory
{
    public static Product Create(ProductEntity entity)
    {
        return new Product
        {
            ArticleNumber = entity.ArticleNumber,
            Title = entity.Title,
            Description = entity.Description,
            IsService = entity.IsService
        };
    }

    public static ProductEntity Create(ProductCreateRequest request)
    {
        return new ProductEntity
        {
            ArticleNumber = request.ArticleNumber,
            Title = request.Title,
            Description = request.Description,
            IsService = request.IsService,
            Created = DateTime.Now,
            Modified = DateTime.Now
        };
    }
}
