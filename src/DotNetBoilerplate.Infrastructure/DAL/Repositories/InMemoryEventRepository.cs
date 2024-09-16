using DotNetBoilerplate.Core.Events;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class InMemoryEventRepository : IEventRepository
{
    private readonly List<Event> _events = [];

    public InMemoryEventRepository()
    {
    }

    public Task<Event?> FindByIdAsync(EventId id)
    {
        return Task.FromResult(_events.Find(x => x.Id == id));
    }

    public Task AddAsync(Event @event)
    {
        _events.Add(@event);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Event @event)
    {
        var eventId = @event.Id;
        var eventToUpdate = _events.Find(x => x.Id == eventId);
        eventToUpdate?.Update(@event.Title, @event.Description, @event.StartDate, @event.EndDate, @event.Location, @event.MaxNumberOfReservations);
        return Task.CompletedTask;
    }

    public Task<List<Event>> GetAllAsync()
    {
        return Task.FromResult(_events);
    }
}