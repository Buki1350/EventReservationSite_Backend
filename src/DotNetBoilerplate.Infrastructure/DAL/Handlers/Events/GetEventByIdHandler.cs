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
: IQueryHandler<GetEventByIdQuery, GetEventByIdResponse>
{
    public async Task<GetEventByIdResponse> HandleAsync(GetEventByIdQuery query)
    {
        var @event = await dbContext.Events
            .AsNoTracking()
            .Where(e => e.Id == query.EventId)
            .Select(e => new GetEventByIdResponse(e.Title, e.Description))
            .FirstOrDefaultAsync();
        
        return new GetEventByIdResponse(@event.Title, @event.Description);
    }
}