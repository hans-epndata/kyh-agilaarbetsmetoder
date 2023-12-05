using Manero.Domain.Interfaces;
using Manero.Domain.Models;
using MediatR;

namespace Manero.Application.Commands;


public record AddProductCommand(string name, string description, decimal price) : IRequest<bool>;


internal class AddProductCommandHandler(IProductService productService) : IRequestHandler<AddProductCommand, bool>
{
    private readonly IProductService _productService = productService;

    public async Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        IProduct product = new Product
        {
            Name = request.name,
            Description = request.description,
            Price = request.price
        };

        return await _productService.AddProductAsync(product);
    }
}
