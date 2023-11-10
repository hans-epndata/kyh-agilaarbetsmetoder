using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace WebApi.Models.Products;

public class Product
{
    public string ArticleNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }

    public static implicit operator Product(ProductEntity entity)
    {
        try
        {
            return new Product
            {
                ArticleNumber = entity.ArticleNumber,
                Name = entity.Name,
                Description = entity.Description,
                Category = entity.Category,
                Price = entity.Price
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
