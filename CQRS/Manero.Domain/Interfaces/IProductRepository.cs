using Manero.Domain.Models;
using System.Linq.Expressions;

namespace Manero.Domain.Interfaces;

public interface IProductRepository
{
    Task<bool> AddAsync(ProductEntity entity);
    
    Task<ProductEntity> GetAsync(Expression<Func<ProductEntity, bool>> expression);
    Task<IEnumerable<ProductEntity>> GetAllAsync();

    Task<bool> UpdateAsync(ProductEntity entity);
    Task<bool> DeleteAsync(Expression<Func<ProductEntity, bool>> expression);

    Task<bool> ExistsAsync(Expression<Func<ProductEntity, bool>> expression);
}
