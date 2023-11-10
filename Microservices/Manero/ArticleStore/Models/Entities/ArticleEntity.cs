using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArticleStore.Models.Entities;

public class ArticleEntity
{
    [Key, Required] public string ArticleNumber { get; set; } = null!;
    public string? EAN { get; set; }
    [Required] public string Name { get; set; } = null!;
    public string? Description { get; set; }
    [Required, Column(TypeName = "money")] public decimal Price { get; set; }
    [Required] public bool IsEnabled { get; set; } = true;
    
    public static implicit operator ArticleEntity(Article article)
    {
        return new ArticleEntity
        {
            ArticleNumber = article.ArticleNumber,
            EAN = article.EAN,
            Name = article.Name,
            Description = article.Description,
            Price = article.Price
        };
    }
}
