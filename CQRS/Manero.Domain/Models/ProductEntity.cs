using Manero.Domain.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Manero.Domain.Models;

public class ProductEntity : IProductEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    [Column(TypeName = "money")] public decimal Price { get; set; }
}
