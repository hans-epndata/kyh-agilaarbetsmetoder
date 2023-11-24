namespace ProductProvider.Models;

public class ProductCreateRequest
{
    public required string ArticleNumber { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public bool IsService { get; set; }
}
