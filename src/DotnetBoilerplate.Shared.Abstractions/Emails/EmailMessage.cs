namespace DotNetBoilerplate.Shared.Abstractions.Emails;

public record EmailMessage(string To, string Subject, string Content);