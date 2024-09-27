using DotNetBoilerplate.Application.Events;
using DotNetBoilerplate.Application.Events.DTOs;
using DotNetBoilerplate.Application.Events.Responses;
using DotNetBoilerplate.Application.Reservations.DTOs;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Handlers.Events;

internal sealed class GetAllEventsWithDetailsHandler(DotNetBoilerplateReadDbContext dbContext)
: IQueryHandler<GetAllEventsWithDetailsQuery, GetAllEventsWithDetailsResponse>
{
    public async Task<GetAllEventsWithDetailsResponse> HandleAsync(GetAllEventsWithDetailsQuery query)
    {
        var events = await dbContext.Events
            .AsNoTracking() 
            .Select(e => new EventDetailsDto(e.Id, e.Title, e.Reservations
                .Select(r => new ReservationDto(r.Id, r.CreatedAt)).ToList()))
            .ToListAsync();

        return new GetAllEventsWithDetailsResponse(events);
    }
}