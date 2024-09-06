using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Events;

namespace DotNetBoilerplate.Infrastructure.DAL;

internal sealed class PostgresUnitOfWork : IUnitOfWork
{
    private readonly DotNetBoilerplateWriteDbContext _dbContext;
    private readonly IDomainEventsDispatcher _domainEventsDispatcher;

    public PostgresUnitOfWork(DotNetBoilerplateWriteDbContext dbContext, IDomainEventsDispatcher domainEventsDispatcher)
    {
        _dbContext = dbContext;
        _domainEventsDispatcher = domainEventsDispatcher;
    }

    public async Task ExecuteAsync(Func<Task> action)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            await action();

            await _domainEventsDispatcher.DispatchEventsAsync();

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}