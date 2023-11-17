namespace WebApi.Models;

public class ProductRegistrationForm
{
    public string ArticleNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}
