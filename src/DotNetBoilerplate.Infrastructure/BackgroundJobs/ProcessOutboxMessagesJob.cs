using System.Diagnostics.CodeAnalysis;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Domain;
using DotNetBoilerplate.Shared.Abstractions.Outbox;
using DotNetBoilerplate.Shared.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Quartz;

namespace DotNetBoilerplate.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob(
    DotNetBoilerplateWriteDbContext dbContext,
    IServiceProvider serviceProvider)
    : IJob
{
    private readonly DbSet<OutboxMessage> _outboxMessages = dbContext.OutboxMessages;

    [SuppressMessage("ReSharper.DPA", "DPA0006: Large number of DB commands", MessageId = "count: 75")]
    public async Task Execute(IJobExecutionContext context)
    {
        var messages = await _outboxMessages
            .Where(m => m.ProcessedDate == null)
            .OrderBy(x => x.OccurredOn)
            .Take(10)
            .ToListAsync();
        
        using var scope = serviceProvider.CreateScope();
        foreach (var outboxMessage in messages)
        {
            var domainNotification = JsonConvert
                .DeserializeObject<IDomainEventNotification<IDomainEvent>>(outboxMessage.Data,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

            if (domainNotification is null) continue;

            var handlerType =
                typeof(IDomainNotificationHandler<>).MakeGenericType(domainNotification.DomainEvent.GetType());
            var handlers = scope.ServiceProvider.GetServices(handlerType);

            var tasks = handlers.Select(x =>
                (Task)handlerType
                    .GetMethod(nameof(IDomainNotificationHandler<IDomainEvent>.HandleAsync))
                    ?.Invoke(x, [domainNotification.DomainEvent]));

            await Task.WhenAll(tasks);

            outboxMessage.ProcessedDate = DateTimeOffset.Now;
        }

        await dbContext.SaveChangesAsync();
    }
}