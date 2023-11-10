namespace WebApp.Models.Products;

public class ProductCreateRequest
{
    public string ArticleNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
}
