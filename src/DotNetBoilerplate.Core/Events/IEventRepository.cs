using DotNetBoilerplate.Core.Reservations;

namespace DotNetBoilerplate.Core.Events;

public interface IEventRepository
{
    Task<Event?> FindByIdAsync(EventId id);
    Task AddAsync(Event @event);
    Task UpdateAsync(Event @event);
    Task<List<Event>> GetAllAsync();
}