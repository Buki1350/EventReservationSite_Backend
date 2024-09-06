using DotNetBoilerplate.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Http;

namespace DotNetBoilerplate.Shared.Contexts;

public class Context : IContext
{
    private Context(IIdentityContext identity = null)
    {
        Identity = identity ?? IdentityContext.Empty;
    }

    public Context(HttpContext context) : this(new IdentityContext(context.User))
    {
    }

    public Guid RequestId { get; } = Guid.NewGuid();
    public IIdentityContext Identity { get; }
}