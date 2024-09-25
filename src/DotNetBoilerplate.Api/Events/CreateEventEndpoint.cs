using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Events.CreateEvent;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Events;

public class CreateEventEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("/", Handle)
            .RequireAuthorization()
            .WithSummary("Creates a new event");
    }

    private static async Task<Ok<Response>> Handle(
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new CreateEventCommand(
            Guid.NewGuid(), 
            request.Title,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.Location,
            request.MaxNumberOfReservations
        );
        
        await commandDispatcher.DispatchAsync(command, ct);

        return TypedResults.Ok(new Response(command.Id, command.Title, command.Description));
    }


    internal sealed record Response(Guid Id, string Title, string Description);
    public sealed class Request
    {
        [Required] public string Title { get; init; }
        [Required] public string Description { get; init; }
        [Required] public DateTime StartDate { get; init; }
        [Required] public DateTime EndDate { get; init; }
        [Required] public string Location { get; init; }
        [Required] public int MaxNumberOfReservations { get; init; }
        
    }
}