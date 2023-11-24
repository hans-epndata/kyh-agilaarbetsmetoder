using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ProductStore.Models;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ProductStore.Services;

public class TokenService(HttpClient http, IConfiguration configuration)
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _configuration = configuration;

    public bool IsValidToken(string accessToken)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();

        try
        {
            tokenHandler.ValidateToken(accessToken, validationParameters, out _);
            return true;
        }
        catch { return false; }
    }

    public async Task<TokenResponse> RenewTokensAsync(string accessToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, _configuration["Jwt:RefreshTokenUrl"])
        {
            Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["access_token"] = accessToken,
            })
        };

        HttpResponseMessage response;
        try
        {
            response = await _http.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException e)
        {
            // Hantera fel här, t.ex. logga och returnera ett anpassat felmeddelande
            throw new Exception("Problem med att förnya tokens.", e);
        }

        var responseContent = await response.Content.ReadAsStringAsync();
        var tokens = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

        return tokens ?? throw new Exception("Kunde inte deserialisera token-svaret.");
    }

    public TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = _configuration["Jwt:Issuer"],
            ValidAudiences = _configuration.GetSection("Jwt:Audiences").Get<IEnumerable<string>>(),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!))
        };
    }
}
