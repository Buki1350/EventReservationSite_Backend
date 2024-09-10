using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Events.AddEvent;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Events;

public sealed class AddEventEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("addEvent", Handle)
            .WithSummary("Adds a new event");
    }

    private static async Task<Ok<Response>> Handle(
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new AddEventCommand(
            Guid.NewGuid(),
            request.OrganizerId,
            request.Title,
            request.Description,
            request.Location,
            request.EventDate,
            request.MaxNumberOfTickets
            );

        await commandDispatcher.DispatchAsync(command, ct);
        
        return TypedResults.Ok(new Response(command.EventId));
    }
    
    internal sealed record Response(
        Guid EventId
        );

    internal sealed class Request
    {
        [Required] public string Title { get; init; }
        [Required] public DateTime EventDate { get; init; }
        [Required] public int MaxNumberOfTickets { get; init; }
        [Required] public Guid OrganizerId { get; init; }
        public string Location { get; init; }
        public string Description { get; init; }
    }
}