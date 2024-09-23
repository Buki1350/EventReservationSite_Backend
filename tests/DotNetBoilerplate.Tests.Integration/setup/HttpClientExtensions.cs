using System.Net.Http.Headers;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Infrastructure.Auth;
using DotNetBoilerplate.Shared.Time;
using Microsoft.Extensions.Options;

namespace DotNetBoilerplate.Tests.Integration.setup;

internal static class HttpClientExtensions
{
    private static readonly Authenticator Authenticator;

    static HttpClientExtensions()
    {
        var options = OptionsHelper.GetOptions<AuthOptions>("auth");

        var myOptions = Options.Create(options);
        Authenticator = new Authenticator(myOptions, new Clock());
    }

    public static void AuthenticateAsAdmin(this HttpClient client, Guid? userId = default)
    {
        var jwtResponse = Authenticator.CreateToken(userId ?? Guid.NewGuid(), Role.Admin());
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse.AccessToken);
    }
}