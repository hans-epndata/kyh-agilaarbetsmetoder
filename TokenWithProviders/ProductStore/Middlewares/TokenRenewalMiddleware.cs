using ProductStore.Services;
using System.Diagnostics;

namespace ProductStore.Middlewares;

public static class TokenRenewalMiddlewareExtensions
{
    public static IApplicationBuilder UseTokenRenewal(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TokenRenewalMiddleware>();
    }
}



public class TokenRenewalMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, TokenService tokenService)
    {

        // kolla om det finns någon accesstoken, om inte fortsätt till nästa middleware
        if (!context.Request.Headers.TryGetValue("Authorization", out var token))
        {
            await _next(context);
            return;
        }

        // om accesstoken är giltig fortsätt till nästa middleware
        var accessToken = token.ToString().Split(' ')[1];
        if (tokenService.IsValidToken(accessToken))
        {
            await _next(context);
            return;
        }

        Debug.WriteLine(accessToken);

        // gör en renew tokens och sätter context till det nya
        var tokens = await tokenService.RenewTokensAsync(accessToken);
        context.Response.Headers.Append("AccessToken", tokens.AccessToken);
        context.Response.Headers.Append("RefreshToken", tokens.RefreshToken);

        context.Request.Headers.Authorization = "Bearer " + tokens.AccessToken;
        await _next(context);
    }
}
