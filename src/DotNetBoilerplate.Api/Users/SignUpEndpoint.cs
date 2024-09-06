using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Users.SignUp;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace DotNetBoilerplate.Api.Users;

internal sealed class SignUpEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("sign-up", Handle)
            .WithSummary("Sign Up");
    }

    private static async Task<Ok<Response>> Handle(
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        CancellationToken ct
    )
    {
        var command = new SignUpCommand(
            Guid.NewGuid(),
            request.Email,
            request.Username,
            request.Password
        );

        await commandDispatcher.DispatchAsync(command, ct);

        return TypedResults.Ok(new Response(command.UserId));
    }

    internal sealed record Response(
        Guid Id
    );

    private sealed class Request
    {
        [Required] public string Email { get; init; }

        [Required] public string Username { get; init; }

        [Required] public string Password { get; init; }
    }
}