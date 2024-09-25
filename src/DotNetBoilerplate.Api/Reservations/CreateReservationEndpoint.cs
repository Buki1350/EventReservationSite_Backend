using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Reservations.CreateReservation;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Reservations;

public class CreateReservationEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/event/{eventId:guid}/reservations/", Handle)
            .RequireAuthorization()
            .WithSummary("Creates new reservation");
    }

    private static async Task<Ok<Response>> Handle(
        [FromRoute] Guid eventId,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new CreateReservationCommand(
            eventId
            );

        await commandDispatcher.DispatchAsync(command, ct);
        
        return TypedResults.Ok(new Response(command.EventId));
    }
    
    internal sealed record Response(Guid EventId);
}