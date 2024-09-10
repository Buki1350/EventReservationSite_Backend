using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Users.SignIn;

public record SignInCommand(string Email, string Password) : ICommand;