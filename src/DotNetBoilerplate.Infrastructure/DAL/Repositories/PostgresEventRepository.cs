﻿using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Repositories;

internal sealed class PostgresEventRepository : IEventRepository
{
    private readonly DbSet<Event> _events;

    public PostgresEventRepository(DotNetBoilerplateWriteDbContext dbContext)
    {
        _events = dbContext.Events;
    }

    public async Task<Event?> FindByIdAsync(EventId id)
    {
        return await _events.SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(Event @event)
    {
        await _events.AddAsync(@event);
    }

    public Task UpdateAsync(Event @event)
    {
        _events.Update(@event);
        return Task.CompletedTask;
    }

    public async Task<List<Event>> GetAllAsync()
    {
        return await _events.ToListAsync();
    }
}