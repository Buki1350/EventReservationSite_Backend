using System.Security.Claims;
using DotNetBoilerplate.Shared.Abstractions.Contexts;

namespace DotNetBoilerplate.Shared.Contexts;

public class IdentityContext : IIdentityContext
{
    private IdentityContext()
    {
    }

    public IdentityContext(ClaimsPrincipal principal)
    {
        if (principal?.Identity is null || string.IsNullOrWhiteSpace(principal.Identity.Name)) return;

        IsAuthenticated = principal.Identity?.IsAuthenticated is true;
        Id = IsAuthenticated ? Guid.Parse(principal.Identity.Name) : Guid.Empty;
        Role = principal?.Claims?.SingleOrDefault(x => x?.Type == ClaimTypes.Role)?.Value;
    }

    public static IIdentityContext Empty => new IdentityContext();
    public bool IsAuthenticated { get; }
    public Guid Id { get; }
    public string Role { get; }
}