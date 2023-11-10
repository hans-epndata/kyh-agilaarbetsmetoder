using ArticleStore.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ArticleStore.Models;

public class Article
{
    [Required] public string ArticleNumber { get; set; } = null!;
    public string? EAN { get; set; }
    [Required] public string Name { get; set; } = null!;
    public string? Description { get; set; }
    [Required] public decimal Price { get; set; }

    public static implicit operator Article(ArticleEntity entity)
    {
        return new Article
        {
            ArticleNumber = entity.ArticleNumber,
            EAN = entity.EAN,
            Name = entity.Name,
            Description = entity.Description,
            Price = entity.Price
        };
    }
}
