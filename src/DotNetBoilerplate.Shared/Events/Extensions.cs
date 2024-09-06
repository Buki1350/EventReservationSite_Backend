using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoilerplate.Shared.Events;

public static class Extensions
{
    public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();
        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
        );

        return services;
    }

    public static IServiceCollection AddDomainNotificationHandlers(this IServiceCollection services)
    {
        var assembly = Assembly.GetCallingAssembly();

        services.Scan(s => s.FromAssemblies(assembly)
            .AddClasses(c => c.AssignableTo(typeof(IDomainNotificationHandler<>)))
            .AsSelfWithInterfaces()
            .WithScopedLifetime()
        );

        return services;
    }
}