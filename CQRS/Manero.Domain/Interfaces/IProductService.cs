using System.Linq.Expressions;

namespace Manero.Domain.Interfaces;

public interface IProductService
{
    Task<bool> AddProductAsync(IProduct product);
    
    Task<IEnumerable<IProduct>> GetAllProductsAsync();
    Task<IProduct> GetProductAsync(Expression<Func<IProduct, bool>> expression);
    Task<bool> UpdateProductAsync(IProduct product);
    Task<bool> DeleteProductAsync(Expression<Func<IProduct, bool>> expression);

}
