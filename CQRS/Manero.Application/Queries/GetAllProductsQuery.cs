using Manero.Domain.Interfaces;
using MediatR;

namespace Manero.Application.Queries;

public record GetAllProductsQuery() : IRequest<IEnumerable<IProduct>>;

internal class GetAllProductsQueryHandler(IProductService productService) : IRequestHandler<GetAllProductsQuery, IEnumerable<IProduct>>
{
    private readonly IProductService _productService = productService;

    public async Task<IEnumerable<IProduct>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        return await _productService.GetAllProductsAsync();
    }
}
