using System.ComponentModel.DataAnnotations;

namespace RESTFUL_WebApi.Models;

public class Product
{

    [Required] public string ArticleNumber { get; set; } = null!;
    [Required] public string Title { get; set; } = null!;
    public string? Description { get; set; }
    [Required] public string Manifacture { get; set; } = null!;
    [Required] public decimal Price { get; set; }
}
