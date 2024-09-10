using DotNetBoilerplate.Application.Security;
using DotNetBoilerplate.Application.Security.Responses;
using Microsoft.AspNetCore.Http;

namespace DotNetBoilerplate.Infrastructure.Auth;

internal sealed class HttpContextTokenStorage : ITokenStorage
{
    private const string TokenKey = "jwt";
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpContextTokenStorage(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void Set(JwtResponse jwt)
    {
        _httpContextAccessor.HttpContext?.Items.TryAdd(TokenKey, jwt);
    }

    public JwtResponse Get()
    {
        if (_httpContextAccessor is null) return null;
        if (_httpContextAccessor.HttpContext != null &&
            _httpContextAccessor.HttpContext.Items.TryGetValue(TokenKey, out var jwt)) return jwt as JwtResponse;

        return null;
    }
}