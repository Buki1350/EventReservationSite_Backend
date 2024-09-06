using DotNetBoilerplate.Shared.Events;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoilerplate.Infrastructure.Events;

internal static class Extensions
{
    public static IServiceCollection AddDomainEventsDispatching(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventsDispatcher, DomainEventsDispatcher>();
        services.AddScoped<IDomainEventsAccessor, DomainEventsAccessor>();

        return services;
    }
}