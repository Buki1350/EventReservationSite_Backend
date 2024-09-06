namespace DotNetBoilerplate.Application.Users.SignIn;

public class SignInRequest
{
    public string Email { get; init; }
    public string Password { get; init; }

    public SignInCommand ToCommand()
    {
        return new SignInCommand(Email, Password);
    }
}