using FluentValidation;

namespace DotNetBoilerplate.Application.Users.SignUp;

internal sealed class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);
    }
}