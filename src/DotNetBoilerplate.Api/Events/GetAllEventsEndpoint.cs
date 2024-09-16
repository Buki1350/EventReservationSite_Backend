using DotNetBoilerplate.Application.Events;
using DotNetBoilerplate.Application.Events.Responses;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using DotNetBoilerplate.Shared.Contexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Events;

public class GetAllEventsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("", Handle)
            .RequireAuthorization()
            .WithSummary("Returns all the events");
    }

    private static async Task<Ok<EventsInfoResponse>> Handle(
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new GetAllEventsQuery();
        var result = await queryDispatcher.QueryAsync(query, ct);
        return TypedResults.Ok(result);
    }
}