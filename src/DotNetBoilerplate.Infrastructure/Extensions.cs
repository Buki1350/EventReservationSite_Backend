using System.Runtime.CompilerServices;
using DotNetBoilerplate.Infrastructure.Auth;
using DotNetBoilerplate.Infrastructure.BackgroundJobs;
using DotNetBoilerplate.Infrastructure.DAL;
using DotNetBoilerplate.Infrastructure.Emails;
using DotNetBoilerplate.Infrastructure.Events;
using DotNetBoilerplate.Infrastructure.Exceptions;
using DotNetBoilerplate.Infrastructure.Security;
using DotNetBoilerplate.Infrastructure.Swagger;
using DotNetBoilerplate.Shared.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
[assembly: InternalsVisibleTo("DotNetBoilerplate.Application")]

namespace DotNetBoilerplate.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(ConfigureSwagger.ConfigureSwaggerOptions)
            .AddPostgres(configuration)
            .AddDomainEventsDispatching()
            .AddBackgroundJobs()
            .AddSecurity()
            .AddExceptions()
            .AddContext()
            .AddAuth(configuration)
            .AddEmails(configuration);

        return services;
    }


    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseMiddleware<ExceptionMiddleware>();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseContext();
        app.MapControllers();

        return app;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}