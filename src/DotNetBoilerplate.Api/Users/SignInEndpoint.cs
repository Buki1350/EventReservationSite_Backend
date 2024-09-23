using System.ComponentModel.DataAnnotations;
using DotNetBoilerplate.Application.Security;
using DotNetBoilerplate.Application.Users.SignIn;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable UnusedAutoPropertyAccessor.Local

namespace DotNetBoilerplate.Api.Users;

internal sealed class SignInEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
    {
        app.MapPost("sign-in", Handle)
            .WithSummary("Sign In");
    }

    private static async Task<Ok<Response>> Handle(
        [FromBody] Request request,
        [FromServices] ICommandDispatcher commandDispatcher,
        [FromServices] ITokenStorage tokenStorage,
        CancellationToken ct
    )
    {
        var command = new SignInCommand(
            request.Email,
            request.Password
        );

        await commandDispatcher.DispatchAsync(command, ct);

        var token = tokenStorage.Get();
        return TypedResults.Ok(new Response(token.AccessToken));
    }

    public sealed record Response(
        string Token
    );

    public sealed class Request
    {
        [Required] public string Email { get; init; }
        [Required] public string Password { get; init; }
    }
}