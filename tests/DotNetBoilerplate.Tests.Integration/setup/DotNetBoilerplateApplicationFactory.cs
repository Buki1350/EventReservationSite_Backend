using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DotNetBoilerplate.Tests.Integration.setup;

public class DotNetBoilerplateApplicationFactory<T> : WebApplicationFactory<T> where T : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, config) =>
        {
            builder.UseEnvironment("IntegrationTests");
            base.ConfigureWebHost(builder);
        });
    }
}