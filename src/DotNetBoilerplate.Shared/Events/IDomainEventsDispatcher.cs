namespace DotNetBoilerplate.Shared.Events;

public interface IDomainEventsDispatcher
{
    Task DispatchEventsAsync();
}