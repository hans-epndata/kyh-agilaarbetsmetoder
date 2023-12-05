using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Manero.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        return services;   
    }
}
