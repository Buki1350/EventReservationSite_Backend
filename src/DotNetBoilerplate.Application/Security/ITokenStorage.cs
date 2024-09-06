using DotNetBoilerplate.Application.Security.Responses;

namespace DotNetBoilerplate.Application.Security;

public interface ITokenStorage
{
    void Set(JwtResponse jwt);
    JwtResponse Get();
}