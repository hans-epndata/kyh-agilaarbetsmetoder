namespace ProductStore.Models;

public class Product
{
    public string ArticleNumber { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsService { get; set; }
}
