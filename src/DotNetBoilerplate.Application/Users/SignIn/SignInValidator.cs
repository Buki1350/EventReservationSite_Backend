using FluentValidation;

namespace DotNetBoilerplate.Application.Users.SignIn;

internal sealed class SignInValidator : AbstractValidator<SignInCommand>
{
    public SignInValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.Password)
            .MinimumLength(2)
            .MaximumLength(50)
            .NotNull();
    }
}