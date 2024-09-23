using Microsoft.Extensions.Configuration;

namespace DotNetBoilerplate.Tests.Integration.setup;

internal sealed class OptionsHelper
{
    private const string AppSettings = "appsettings.IntegrationTests.json";

    public static TOptions GetOptions<TOptions>(string sectionName) where TOptions : class, new()
    {
        var options = new TOptions();
        var configuration = GetConfigurationRoot();
        var section = configuration.GetSection(sectionName);

        GetConfigurationRoot()
            .GetSection(sectionName);

        section.Bind(options);

        return options;
    }

    private static IConfigurationRoot GetConfigurationRoot()
    {
        return new ConfigurationBuilder()
            .AddJsonFile(AppSettings)
            .AddEnvironmentVariables()
            .Build();
    }
}