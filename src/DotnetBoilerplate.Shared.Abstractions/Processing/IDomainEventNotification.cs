namespace DotNetBoilerplate.Shared.Abstractions.Processing;

public interface IDomainEventNotification<out TEventType>
{
    TEventType DomainEvent { get; }
}