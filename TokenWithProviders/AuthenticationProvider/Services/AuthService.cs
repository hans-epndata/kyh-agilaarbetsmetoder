using AuthenticationProvider.Enums;
using AuthenticationProvider.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthenticationProvider.Services;

public class AuthService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
{
    private readonly UserManager<ApplicationUser> _userManager = userManager;
    private readonly IConfiguration _configuration = configuration;

    public async Task<ServiceStatusCode> SignUpAsync(SignUpForm form)
    {
        var existingUser = await _userManager.FindByEmailAsync(form.Email);
        if (existingUser == null)
        {
            var user = new ApplicationUser
            {
                Email = form.Email,
                UserName = form.Email
            };

            var result = await _userManager.CreateAsync(user, form.Password);
            if (result.Succeeded)
                return ServiceStatusCode.Success;

            return ServiceStatusCode.Error;
        }

        return ServiceStatusCode.AlreadyExists;
    }

    public async Task<AuthServiceResult> SignInAsync(SignInForm form)
    {
        var user = await _userManager.FindByEmailAsync(form.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, form.Password))
        {
            user.RefreshToken = TokenGenerator.GenerateRefreshToken();
            user.RefreshTokenExpiryDate = DateTime.Now.AddDays(60);
            await _userManager.UpdateAsync(user);

            var accessToken = TokenGenerator.GenerateJwtToken(user, _configuration);
            return new AuthServiceResult { AccessToken = accessToken, RefreshToken = user.RefreshToken };
        }

        return new AuthServiceResult { StatusCode = ServiceStatusCode.NotFound };
    }

    public async Task<AuthServiceResult> RefreshTokenAsync(HttpContext httpContext)
    {
        var accessTokenHeader = httpContext.Request.Headers.Authorization.ToString();
        if (string.IsNullOrEmpty(accessTokenHeader) || !accessTokenHeader.StartsWith("Bearer "))
            return new AuthServiceResult { StatusCode = ServiceStatusCode.Error };

        var accessToken = accessTokenHeader["Bearer ".Length..].Trim();
        var refreshTokenCookie = httpContext.Request.Cookies["refreshToken"];

        var principal = TokenGenerator.GetClaimsPrincipalFromToken(accessToken, _configuration);
        var userName = principal.Identity!.Name;
        var user = await _userManager.FindByEmailAsync(userName!);

        if (user  == null || user.RefreshToken != refreshTokenCookie || user.RefreshTokenExpiryDate <= DateTime.Now)
        {
            return new AuthServiceResult { StatusCode = ServiceStatusCode.Error };
        }

        user.RefreshToken = TokenGenerator.GenerateRefreshToken();
        user.RefreshTokenExpiryDate = DateTime.Now.AddDays(60);
        await _userManager.UpdateAsync(user);

        accessToken = TokenGenerator.GenerateJwtToken(user, _configuration);

        httpContext.Response.Cookies.Append("refreshToken", user.RefreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.Now.AddDays(60)
        });

        return new AuthServiceResult
        {
            AccessToken = accessToken,
            RefreshToken = user.RefreshToken
        };
    }
}
