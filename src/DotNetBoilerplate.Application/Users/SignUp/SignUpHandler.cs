using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Application.Security;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Time;

namespace DotNetBoilerplate.Application.Users.SignUp;

internal sealed class SignUpHandler : ICommandHandler<SignUpCommand>
{
    private readonly IClock _clock;
    private readonly IPasswordManager _passwordManager;
    private readonly IUserReadService _userReadService;
    private readonly IUserRepository _userRepository;

    public SignUpHandler(IUserRepository userRepository, IUserReadService userReadService,
        IPasswordManager passwordManager, IClock clock)
    {
        _userRepository = userRepository;
        _userReadService = userReadService;
        _passwordManager = passwordManager;
        _clock = clock;
    }

    public async Task HandleAsync(SignUpCommand command)
    {
        var email = new Email(command.Email);
        var username = new Username(command.Username);
        var password = new Password(_passwordManager.Secure(command.Password));

        if (await _userReadService.ExistsByEmailAsync(email)) throw new EmailExistsException();
        if (await _userReadService.ExistsByUsernameAsync(username)) throw new UsernameExistsException();

        var user = User.New(command.UserId, email, username, password, _clock.Now());
        await _userRepository.AddAsync(user);
    }
}