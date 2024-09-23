using DotNetBoilerplate.Application.Events;
using DotNetBoilerplate.Application.Events.DTOs;
using DotNetBoilerplate.Application.Events.Responses;
using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Application.Events.GetAllEvents;

internal sealed class GetAllEventsHandler(
    DotNetBoilerplateReadDbContext dbContext
)
    : IQueryHandler<GetAllEventsQuery, EventsInfoResponse>
{
    public async Task<EventsInfoResponse> HandleAsync(GetAllEventsQuery query)
    {
        var events = await dbContext.Events
            .AsNoTracking() 
            .ToListAsync(); 

        var eventDtos = events.Select(e => new EventDto
        {
          Id  = e.EventId,
          Title = e.Title,
        }).ToList();

        return new EventsInfoResponse(eventDtos);
    }
}