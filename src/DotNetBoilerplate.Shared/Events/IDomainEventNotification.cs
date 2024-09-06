using DotNetBoilerplate.Shared.Abstractions.Domain;

namespace DotNetBoilerplate.Shared.Events;

//marker
public interface IDomainEventNotification
{
    Guid Id { get; }
}

public interface IDomainEventNotification<out TEventType> : IDomainEventNotification
    where TEventType : IDomainEvent
{
    TEventType DomainEvent { get; }
}