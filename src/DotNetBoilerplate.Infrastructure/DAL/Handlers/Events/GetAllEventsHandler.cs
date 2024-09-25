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
    : IQueryHandler<GetAllEventsQuery, GetAllEventsResponse>
{
    public async Task<GetAllEventsResponse> HandleAsync(GetAllEventsQuery query)
    {
        var events = await dbContext.Events
            .AsNoTracking() 
            .Select(e => new EventDto(e.Id, e.Title))
            .ToListAsync(); 
        
        return new GetAllEventsResponse(events);
    }
}