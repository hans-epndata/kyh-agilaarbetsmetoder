using Azure.Core;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApi.Models;
using WebApi.Models.Entities;
using WebApi.Repositories;

namespace WebApi.Services;

public interface ITokenGenerator
{
    string GenerateAccessToken(Claim[] claims, int expiresInMinutes = 15);
    Task<string> GenerateRefreshToken(string userId);
    Task<TokenResponse> GenerateTokensAsync(Claim[] claims);
    Task<TokenResponse> RefreshTokenAsync(TokenRequest request);
    ClaimsPrincipal ValidateAccessToken(string accessToken);
    Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken);
}

public class TokenGenerator(IConfiguration configuration, ITokenRepository tokenRepository) : ITokenGenerator
{
    private readonly IConfiguration _configuration = configuration;
    private readonly ITokenRepository _tokenRepository = tokenRepository;


    public async Task<TokenResponse> RefreshTokenAsync(TokenRequest request)
    {
        try
        {
            var principal = ValidateAccessToken(request.AccessToken);
            if (principal != null)
            {
                var tokens = await GenerateTokensAsync(principal.Claims.ToArray());
                return tokens;
            }

            var result = await ValidateRefreshTokenAsync(request.UserId, request.RefreshToken);
            if (result)
            {
                var tokens = await GenerateTokensAsync([new Claim("UserId", request.UserId)]);
                return tokens;
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }




    public async Task<string> GenerateRefreshToken(string userId)
    {
        try
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            var refreshToken = new TokenEntity
            {
                UserId = userId,
                Token = Convert.ToBase64String(randomNumber),
                ExpiresAt = DateTime.UtcNow.AddMonths(Convert.ToInt32(_configuration["RefreshToken:ExpiresMonth"]))
            };

            var result = await _tokenRepository.SetRefreshTokenAsync(refreshToken);
            return result.Token ??= null!;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }


    public string GenerateAccessToken(Claim[] claims, int expiresInMinutes = 15)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(expiresInMinutes),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!)),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var accessToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(accessToken);
        }
        catch (Exception ex) 
        { 
            Debug.WriteLine(ex.Message); 
        }
        return null!;
    }

    public ClaimsPrincipal ValidateAccessToken(string accessToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _configuration["Jwt:Issuer"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!)),

                ValidateAudience = true,
                ValidAudience = _configuration["Jwt:Audience"],

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var prinipal = tokenHandler.ValidateToken(accessToken, validationParameters, out _);
            return prinipal;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public async Task<bool> ValidateRefreshTokenAsync(string userId, string refreshToken)
    {
        try
        {
            var result = await _tokenRepository.GetRefreshTokenAsync(userId);
            if (result != null)
                return result.Token == refreshToken && result.ExpiresAt > DateTime.UtcNow;

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public async Task<TokenResponse> GenerateTokensAsync(Claim[] claims)
    {
        try
        {
            var tokenResponse = new TokenResponse
            {
                AccessToken = GenerateAccessToken(claims),
                RefreshToken = await GenerateRefreshToken(claims.FirstOrDefault(x => x.Type == "UserId")?.Value ?? null!)
            };

            return tokenResponse;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }
}
