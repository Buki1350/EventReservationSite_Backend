using DotNetBoilerplate.Application.Events;
using DotNetBoilerplate.Application.Events.DTOs;
using DotNetBoilerplate.Application.Events.Responses;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Handlers.Events;

internal sealed class GetAllEventsHandler(
    DotNetBoilerplateReadDbContext dbContext
)
    : IQueryHandler<GetAllEventsQuery, EventsInfoResponse>
{
    public async Task<EventsInfoResponse> HandleAsync(GetAllEventsQuery query)
    {
        var events = await dbContext.Events
            .AsNoTracking() 
            .Select(e => new EventDto
                {
                    Id = e.Id,
                    Title = e.Title,
                }
            )
            .ToListAsync(); 
        
        return new EventsInfoResponse(events);
    }
}