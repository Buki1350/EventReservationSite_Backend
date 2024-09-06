using DotNetBoilerplate.Shared.Abstractions.Domain;

namespace DotNetBoilerplate.Infrastructure.Events;

public interface IDomainEventsAccessor
{
    IReadOnlyCollection<IDomainEvent> GetAllDomainEvents();
    void ClearAllDomainEvents();
}