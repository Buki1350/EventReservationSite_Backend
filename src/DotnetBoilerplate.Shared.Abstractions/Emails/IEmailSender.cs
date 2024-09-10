namespace DotNetBoilerplate.Shared.Abstractions.Emails;

public interface IEmailSender
{
    Task SendEmailAsync(EmailMessage message);
}