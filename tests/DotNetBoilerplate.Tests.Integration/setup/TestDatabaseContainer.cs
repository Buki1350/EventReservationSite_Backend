using Testcontainers.PostgreSql;

namespace DotNetBoilerplate.Tests.Integration.setup;

public sealed class TestDatabaseContainer
{
    private const string Username = "admin";
    private const string Password = "$3cureP@ssw0rd";
    private const string Database = "dot-net-boilerplate-test";
    private PostgreSqlContainer? _container;
    public string ConnectionString { get; private set; }

    public async Task InitializeAsync()
    {
        _container = new PostgreSqlBuilder()
            .WithImage("postgres:latest")
            .WithDatabase(Database)
            .WithUsername(Username)
            .WithPassword(Password)
            .Build();

        await _container!.StartAsync();

        ConnectionString = _container.GetConnectionString();
    }

    public async Task DisposeAsync()
    {
        await _container!.StopAsync();
    }
}