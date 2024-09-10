using DotNetBoilerplate.Shared.Abstractions.Time;
using DotNetBoilerplate.Shared.Time;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoilerplate.Shared;

public static class Extensions
{
    public static IServiceCollection AddShared(this IServiceCollection services)
    {
        services.AddSingleton<IClock, Clock>();
        return services;
    }
}