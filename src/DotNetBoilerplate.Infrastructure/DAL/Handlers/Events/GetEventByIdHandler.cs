using DotNetBoilerplate.Application.Events;
using DotNetBoilerplate.Application.Events.Responses;
using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Handlers.Events;

internal sealed class GetEventByIdHandler(
    DotNetBoilerplateReadDbContext dbContext
    )
: IQueryHandler<GetEventByIdQuery, EventInfoResponse>
{
    public async Task<EventInfoResponse> HandleAsync(GetEventByIdQuery query)
    {
        var @event = await dbContext.Events
            .AsNoTracking()
            .Where(e => e.Id == query.EventId)
            .Select(e => new EventInfoResponse{Title = e.Title, Description = e.Description})
            .FirstOrDefaultAsync();
        
        //.FirstOrDefaultAsync(e => e.Id == query.EventId);
        return new EventInfoResponse {Title = @event.Title, Description = @event.Description};
    }
}