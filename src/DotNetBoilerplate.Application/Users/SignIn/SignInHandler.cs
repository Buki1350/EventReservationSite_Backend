using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Application.Security;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Users.SignIn;

public sealed class SignInHandler : ICommandHandler<SignInCommand>
{
    private readonly IAuthenticator _authenticator;
    private readonly IPasswordManager _passwordManager;
    private readonly ITokenStorage _tokenStorage;
    private readonly IUserReadService _userReadService;

    public SignInHandler(IAuthenticator authenticator,
        IPasswordManager passwordManager, ITokenStorage tokenStorage, IUserReadService userReadService)
    {
        _authenticator = authenticator;
        _passwordManager = passwordManager;
        _tokenStorage = tokenStorage;
        _userReadService = userReadService;
    }


    public async Task HandleAsync(SignInCommand command)
    {
        var user = await _userReadService.GetByEmailAsync(command.Email);
        if (user is null) throw new InvalidCredentialsException();

        if (!_passwordManager.Validate(command.Password, user.Password)) throw new InvalidCredentialsException();

        var jwt = _authenticator.CreateToken(user.Id, user.Role);

        _tokenStorage.Set(jwt);
    }
}