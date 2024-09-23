using DotNetBoilerplate.Application.Events;
using DotNetBoilerplate.Application.Events.Responses;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Events;

public class GetEventByIdEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("/{eventId:guid}", Handle)
            .RequireAuthorization()
            .WithSummary("Returns the event");
    }

    private static async Task<Ok<EventInfoResponse>> Handle(
        [FromServices] IQueryDispatcher queryDispatcher,
        [FromRoute] Guid eventId,
        CancellationToken ct
    )
    {
        var query = new GetEventByIdQuery{EventId = eventId};
        var result = await queryDispatcher.QueryAsync(query, ct);
        return TypedResults.Ok(result);
    }
}