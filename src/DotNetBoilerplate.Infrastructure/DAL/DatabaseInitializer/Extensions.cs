using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoilerplate.Infrastructure.DAL.DatabaseInitializer;

internal static class Extensions
{
    private const string SectionName = "databaseInitializer";

    public static IServiceCollection AddDatabaseInitializer(this IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<DatabaseInitializerOptions>(section);

        services.AddHostedService<DatabaseInitializer>();

        return services;
    }
}