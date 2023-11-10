using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace WebApi.Models.Products;

public class ProductEntity
{
    [Key]
    public string ArticleNumber { get; set; } = null!;

    [Required]
    [Column(TypeName = "nvarchar(450)")]
    public string Name { get; set; } = null!;
    
    public string? Description { get; set; }
    public string? Category { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }
    
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Modified { get; set; } = DateTime.Now;

    public static implicit operator ProductEntity(ProductCreateRequest request)
    {
        try
        {
            return new ProductEntity
            {
                ArticleNumber = request.ArticleNumber,
                Name = request.Name,
                Description = request.Description,
                Category = request.Category,
                Price = request.Price
            };
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
