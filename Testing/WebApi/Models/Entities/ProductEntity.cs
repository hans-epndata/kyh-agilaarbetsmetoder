using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Entities;

public class ProductEntity
{
    [Key]
    public string ArticleNumber { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsEnabled { get; set; } = true;
}
