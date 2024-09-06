using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Domain;

namespace DotNetBoilerplate.Infrastructure.Events;

internal sealed class DomainEventsAccessor : IDomainEventsAccessor
{
    private readonly DotNetBoilerplateWriteDbContext _dbContext;

    public DomainEventsAccessor(DotNetBoilerplateWriteDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IReadOnlyCollection<IDomainEvent> GetAllDomainEvents()
    {
        var domainEntities = _dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any())
            .ToList();

        return domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();
    }

    public void ClearAllDomainEvents()
    {
        var domainEntities = _dbContext.ChangeTracker
            .Entries<Entity>()
            .Where(x => x.Entity.DomainEvents.Any()).ToList();

        domainEntities
            .ForEach(x => x.Entity.ClearDomainEvents());
    }
}