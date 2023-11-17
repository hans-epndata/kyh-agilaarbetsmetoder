using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Diagnostics;
using System.Linq.Expressions;
using WebApi.Contexts;
using WebApi.Models.Entities;

namespace WebApi.Repositories;

public interface IProductRepository
{
    Task<ProductEntity> CreateAsync(ProductEntity entity);
    Task<IEnumerable<ProductEntity>> GetAllAsync();
    Task<ProductEntity> GetAsync(Expression<Func<ProductEntity, bool>> expression);
    Task<bool> ExistsAsync(Expression<Func<ProductEntity, bool>> expression);
}

public class ProductRepository(DataContext context) : IProductRepository
{
    private readonly DataContext _context = context;

    public async Task<ProductEntity> CreateAsync(ProductEntity entity)
    {
        try
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> ExistsAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        try
        {
            return await _context.Products.AnyAsync(expression);
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public async Task<IEnumerable<ProductEntity>> GetAllAsync()
    {
        try
        {
            return await _context.Products.ToListAsync();
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<ProductEntity> GetAsync(Expression<Func<ProductEntity, bool>> expression)
    {
        try
        {
            var result = await _context.Products.FirstOrDefaultAsync(expression);
            return result ??= null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}


