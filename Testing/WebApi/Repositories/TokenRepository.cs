using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApi.Contexts;
using WebApi.Models.Entities;

namespace WebApi.Repositories;

public interface ITokenRepository
{
    Task<TokenEntity> GetRefreshTokenAsync(string userId);
    Task<TokenEntity> SetRefreshTokenAsync(TokenEntity tokenEntity);
}

public class TokenRepository(DataContext context) : ITokenRepository
{
    private readonly DataContext _context = context;

    public async Task<TokenEntity> SetRefreshTokenAsync(TokenEntity tokenEntity)
    {
        try
        {
            var result = await _context.Tokens.FirstOrDefaultAsync(x => x.UserId == tokenEntity.UserId);
            if (result != null)
            {
                result = tokenEntity;
                _context.Tokens.Update(result);
                await _context.SaveChangesAsync();
            }
            else
            {
                result = tokenEntity;
                await _context.Tokens.AddAsync(result);
                await _context.SaveChangesAsync();
            }

            return result;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<TokenEntity> GetRefreshTokenAsync(string userId)
    {
        try
        {
            var result = await _context.Tokens.FirstOrDefaultAsync(x => x.UserId == userId);
            return result ??= null!;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
