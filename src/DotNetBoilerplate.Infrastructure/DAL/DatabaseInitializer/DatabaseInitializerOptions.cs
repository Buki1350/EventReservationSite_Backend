namespace DotNetBoilerplate.Infrastructure.DAL.DatabaseInitializer;

internal record struct DatabaseInitializerAdminOptions
{
    public string Email { get; set; }
    public string Password { get; set; }
}

internal sealed class DatabaseInitializerOptions
{
    public DatabaseInitializerAdminOptions Admin { get; set; }
}