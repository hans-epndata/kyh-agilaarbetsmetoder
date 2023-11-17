using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;
using WebApi.Contexts;
using WebApi.Models.Entities;

namespace WebApi.Repositories;

public interface IUserRepository
{
    Task<UserEntity> AddAsync(UserEntity entity);
    Task<UserEntity> GetAsync(Expression<Func<UserEntity, bool>> predicate);
}

public class UserRepository(DataContext context) : IUserRepository
{
    private readonly DataContext _context = context;

    public async Task<UserEntity> GetAsync(Expression<Func<UserEntity, bool>> predicate)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(predicate);
            return user ??= null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<UserEntity> AddAsync(UserEntity entity)
    {
        try
        {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity ??= null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
