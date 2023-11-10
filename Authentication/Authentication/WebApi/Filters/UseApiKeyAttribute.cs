using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;
using WebApi.Helpers.Misc;
using WebApi.Helpers.Repositories;
using WebApi.Models.Keys;

namespace WebApi.Filters;

public class UseApiKeyAttribute : Attribute, IAsyncActionFilter
{

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var _apiKeyRepository = context.HttpContext.RequestServices.GetRequiredService<ApiKeyRepository>();

        if (context.HttpContext.Request.Query.TryGetValue("code", out var code))
        {
            if(!string.IsNullOrEmpty(code))
            {
                var apiKey = JsonSerializer.Deserialize<KeyEntity>(Base64.Decode(code!));
                if (apiKey != null)
                {
                    if (await _apiKeyRepository.ExistsAsync(x => x.UserId == apiKey.UserId && x.Key == code.ToString()))
                        await next();
                }
            }
        }

        context.Result = new UnauthorizedResult();
        return;
    }
}
