using System.Runtime.CompilerServices;
using DotNetBoilerplate.Shared.Commands;
using DotNetBoilerplate.Shared.Events;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("DotNetBoilerplate.Tests.Unit")]

namespace DotNetBoilerplate.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddCommands();
        services.AddDomainEventHandlers();
        services.AddDomainNotificationHandlers();

        return services;
    }
}