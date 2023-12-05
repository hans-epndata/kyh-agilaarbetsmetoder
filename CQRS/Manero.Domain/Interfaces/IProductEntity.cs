namespace Manero.Domain.Interfaces;

public interface IProductEntity
{
    int Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    decimal Price { get; set; }
}
