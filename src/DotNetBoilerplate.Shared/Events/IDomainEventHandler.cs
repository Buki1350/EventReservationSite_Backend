using DotNetBoilerplate.Shared.Abstractions.Domain;

namespace DotNetBoilerplate.Shared.Events;

public interface IDomainEventHandler<in TEvent> where TEvent : class, IDomainEvent
{
    Task HandleAsync(TEvent domainEvent);
}