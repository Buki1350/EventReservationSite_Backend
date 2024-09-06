using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Users.SignUp;

public record SignUpCommand(Guid UserId, string Email, string Username, string Password) : ICommand;