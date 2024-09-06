using DotNetBoilerplate.Shared.Abstractions.Domain;

namespace DotNetBoilerplate.Shared.Events;

public interface IDomainNotificationHandler<in TEvent>
    where TEvent : class, IDomainEvent
{
    Task HandleAsync(TEvent notification);
}