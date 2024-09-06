using DotNetBoilerplate.Application.Security.Responses;

namespace DotNetBoilerplate.Application.Security;

public interface IAuthenticator
{
    JwtResponse CreateToken(Guid userId, string role);
}