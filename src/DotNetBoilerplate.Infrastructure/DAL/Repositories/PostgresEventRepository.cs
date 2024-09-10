using DotNetBoilerplate.Core.Event;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class PostgresEventRepository : IEventRepository
{
    private readonly DbSet<Event> _events;

    public PostgresEventRepository(DotNetBoilerplateWriteDbContext dbContext)
    {
        _events = dbContext.Events;
    }

    public async Task AddAsync(Event newEvent)
    {
        await _events.AddAsync(newEvent);
    }

    public async Task<Event> FindByIdAsync(EventId id)
    {
        return await _events.SingleOrDefaultAsync(x => x.Id == id);
    }

    public Task UpdateAsync(Event updatedEvent)
    {
        _events.Update(updatedEvent);
        return Task.CompletedTask;
    }
}