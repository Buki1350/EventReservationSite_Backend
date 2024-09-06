using DotNetBoilerplate.Application.Security;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace DotNetBoilerplate.Infrastructure.DAL.DatabaseInitializer;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly DatabaseInitializerAdminOptions _adminOptions;
    private readonly IClock _clock;
    private readonly IPasswordManager _passwordManager;
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IOptions<DatabaseInitializerOptions> options, IServiceProvider serviceProvider,
        IClock clock, IPasswordManager passwordManager)
    {
        _serviceProvider = serviceProvider;
        _clock = clock;
        _passwordManager = passwordManager;
        _adminOptions = options.Value.Admin;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<DotNetBoilerplateWriteDbContext>();
            dbContext.Database.Migrate();

            var adminIsInDatabase = dbContext.Users.Any(user => user.Role.Equals(Role.Admin()));

            if (!adminIsInDatabase)
                dbContext.Users.Add(User.NewAdmin(Guid.NewGuid(), _adminOptions.Email,
                    _passwordManager.Secure(_adminOptions.Password),
                    _clock.Now()));

            dbContext.SaveChanges();
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}