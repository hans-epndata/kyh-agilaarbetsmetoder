namespace WebApi.Models;

public class ProductUpdateForm
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsEnabled { get; set; }
}
