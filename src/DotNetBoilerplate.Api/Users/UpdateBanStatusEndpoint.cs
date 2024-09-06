using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Users.UpdateBanStatus;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Users;

internal sealed class UpdateBanStatusEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPut("{userid:guid}/banned", Handle) //put /userid/banned
            .RequireAuthorization()
            .WithName("banUser");
    }

    private static async Task<Ok<Response>> Handle(
        [FromBody] Request request,
        [FromRoute] Guid userId,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new UpdateBanStatsCommand(
            UserId: userId,
            request.IsBanned
        );
        
        await commandDispatcher.DispatchAsync(command, ct);

        return TypedResults.Ok(new Response(command.UserId));
    }

    internal sealed class Response(
        Guid Id
        );

    private sealed class Request
    {
        [Required] public bool IsBanned { get; set; }
    }
}