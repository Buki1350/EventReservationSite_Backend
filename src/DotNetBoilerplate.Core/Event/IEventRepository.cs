namespace DotNetBoilerplate.Core.Event;

public interface IEventRepository
{
    Task<Event> FindByIdAsync(EventId id);
    Task AddAsync(Event newEvent);
    Task UpdateAsync(Event updatedEvent);
}