using DotNetBoilerplate.Shared.Abstractions.Emails;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoilerplate.Infrastructure.Emails;

internal static class Extensions
{
    private const string SectionName = "emails";

    public static IServiceCollection AddEmails(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        services.Configure<EmailsOptions>(section);

        var options = configuration.GetOptions<EmailsOptions>(SectionName);
        services.AddScoped<IEmailSender, EmailSender>(x => new EmailSender(options));

        return services;
    }
}