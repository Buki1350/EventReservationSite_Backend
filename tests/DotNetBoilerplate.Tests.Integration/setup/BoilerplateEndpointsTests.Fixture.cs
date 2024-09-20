using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Npgsql;
using Respawn;
using Xunit;

namespace DotNetBoilerplate.Tests.Integration.setup;

public sealed class BoilerplateEndpointsTestsFixture : IAsyncLifetime
{
    private const string DbConnectionStringName = "database:connectionString";
    private WebApplicationFactory<Program> _applicationFactory = null!;
    private TestDatabaseContainer _dbContainer = null!;
    private Respawner? _respawner;
    internal IServiceProvider ServiceProvider;
    internal NpgsqlConnection DbConnection { get; set; }
    public HttpClient Client { get; private set; } = null!;

    public async Task InitializeAsync()
    {
        _dbContainer = new TestDatabaseContainer();
        await _dbContainer.InitializeAsync();

        _applicationFactory = new DotNetBoilerplateApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services => { });
                builder.UseSetting(DbConnectionStringName, _dbContainer.ConnectionString);
            });

        DbConnection = new NpgsqlConnection(_dbContainer.ConnectionString);

        Client = _applicationFactory.CreateClient();

        ServiceProvider = _applicationFactory.Services;

        await DbConnection!.OpenAsync();
        await InitializeRespawnerAsync();
    }

    public async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
    }

    internal async Task ResetDbChangesAsync()
    {
        await _respawner!.ResetAsync(DbConnection!);
    }

    private async Task InitializeRespawnerAsync()
    {
        _respawner = await Respawner.CreateAsync(DbConnection!, new RespawnerOptions
        {
            DbAdapter = DbAdapter.Postgres,
            SchemasToInclude = []
        });
    }
}