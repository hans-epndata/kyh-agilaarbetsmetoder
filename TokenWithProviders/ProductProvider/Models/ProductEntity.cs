using System.ComponentModel.DataAnnotations;

namespace ProductProvider.Models;

public class ProductEntity
{
    [Key]
    public required string ArticleNumber { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }

    public DateTime Created {  get; set; }
    public DateTime Modified { get; set; } = DateTime.Now;
    public bool IsEnabled { get; set; } = true;
    public bool IsService { get; set; } = false;
}
