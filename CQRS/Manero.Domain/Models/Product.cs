using Manero.Domain.Interfaces;

namespace Manero.Domain.Models;

public class Product : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
}
