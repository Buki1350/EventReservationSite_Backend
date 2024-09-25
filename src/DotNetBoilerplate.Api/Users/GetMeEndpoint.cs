using DotNetBoilerplate.Application.Users;
using DotNetBoilerplate.Application.Users.Responses;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Api.Users;

internal sealed class GetMeEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapGet("me", Handle)
            .RequireAuthorization()
            .WithSummary("Get Me");
    }

    private static async Task<Results<Ok<UserDetailsResponse>, NotFound>> Handle(
        [FromServices] IQueryDispatcher queryDispatcher,
        [FromServices] IContext context,
        CancellationToken ct
    )
    {
        var query = new GetUserQuery(context.Identity.Id);
        var result = await queryDispatcher.QueryAsync(query, ct);

        if (result is null) return TypedResults.NotFound();

        return TypedResults.Ok(result)!;
    }
}