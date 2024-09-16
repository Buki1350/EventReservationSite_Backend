using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Events.UpdateEvent;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBoilerplate.Api.Events;

public class UpdateEventEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("updateEvent", Handle)
            .WithSummary("Update event");
    }

    private static async Task<Ok<Response>> Handle(
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
        )
    {
        var command = new UpdateEventCommand(
            request.Id,
            request.NewTitle,
            request.NewDescription,
            request.NewStartDate,
            request.NewEndDate,
            request.NewLocation,
            request.NewMaxNumberOfReservations
        );

        await commandDispatcher.DispatchAsync(command, ct);

        return TypedResults.Ok(new Response(command.Id));
    }


    internal sealed class Response(Guid id);

    private sealed class Request
    {
        [Required] public Guid Id { get; init; }
        public string NewTitle { get; init; }
        public string NewDescription { get; init; }
        public DateTime NewStartDate { get; init; }
        public DateTime NewEndDate { get; init; }
        public string NewLocation { get; init; }
        public int NewMaxNumberOfReservations { get; init; }
        
    }
}