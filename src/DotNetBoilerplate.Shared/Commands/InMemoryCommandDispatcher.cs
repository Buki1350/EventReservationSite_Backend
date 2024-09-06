using DotNetBoilerplate.Shared.Abstractions.Commands;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetBoilerplate.Shared.Commands;

public class InMemoryCommandDispatcher : ICommandDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public InMemoryCommandDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync<TCommand>(TCommand command, CancellationToken cancellationToken = default)
        where TCommand : class, ICommand
    {
        using var scope = _serviceProvider.CreateScope();
        var validator = scope.ServiceProvider.GetService<IValidator<TCommand>>();

        if (validator is not null) await validator.ValidateAndThrowAsync(command, cancellationToken);

        var handler = scope.ServiceProvider.GetRequiredService<ICommandHandler<TCommand>>();

        await handler.HandleAsync(command);
    }
}