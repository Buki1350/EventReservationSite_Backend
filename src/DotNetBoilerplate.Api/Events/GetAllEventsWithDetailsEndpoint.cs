using DotNetBoilerplate.Application.Events;
using DotNetBoilerplate.Application.Events.Responses;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Events;

public class GetAllEventsWithDetailsEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("details", Handle)
            .RequireAuthorization()
            .WithSummary("Returns all the events with details");
    }

    private static async Task<Ok<GetAllEventsWithDetailsResponse>> Handle(
        [FromServices] IQueryDispatcher queryDispatcher,
        CancellationToken ct
    )
    {
        var query = new GetAllEventsWithDetailsQuery();
        var result = await queryDispatcher.QueryAsync(query, ct);
        return TypedResults.Ok(result);
    }
}