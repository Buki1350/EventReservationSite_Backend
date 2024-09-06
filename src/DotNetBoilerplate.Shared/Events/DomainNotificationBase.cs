using DotNetBoilerplate.Shared.Abstractions.Domain;

namespace DotNetBoilerplate.Shared.Events;

public class DomainNotificationBase<T> : IDomainEventNotification<T>
    where T : IDomainEvent
{
    public DomainNotificationBase(T domainEvent, Guid id)
    {
        Id = id;
        DomainEvent = domainEvent;
    }

    public DomainNotificationBase()
    {
    }

    public T DomainEvent { get; set; }
    public Guid Id { get; }
}