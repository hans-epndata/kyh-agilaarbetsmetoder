using Manero.Domain.Interfaces;
using Manero.Domain.Models;
using Manero.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Manero.Infrastructure.Repositories;

public class ProductRepository(DataContext context) : IProductRepository
{
    private readonly DataContext _context = context;

    public async Task<bool> AddAsync(ProductEntity entity)
    {
        try
        {
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch { }
        return false;
    }

    public async Task<bool> DeleteAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistsAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        try
        {
            return await _context.Products.AnyAsync(expression);
        }
        catch { }
        return false;
    }

    public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        try
        {
            return await _context.Products.ToListAsync();
        }
        catch { }
        return null!;
    }

    public async Task<ProductEntity> GetAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(ProductEntity entity)
    {
        throw new NotImplementedException();
    }
}
