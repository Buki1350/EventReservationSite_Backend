using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Domain;
using DotNetBoilerplate.Shared.Abstractions.Outbox;
using DotNetBoilerplate.Shared.Abstractions.Time;
using DotNetBoilerplate.Shared.Events;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace DotNetBoilerplate.Infrastructure.Events;

internal sealed class DomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IClock _clock;
    private readonly DotNetBoilerplateWriteDbContext _dbContext;
    private readonly IDomainEventsAccessor _domainEventsAccessor;
    private readonly IServiceProvider _serviceProvider;

    public DomainEventsDispatcher(DotNetBoilerplateWriteDbContext dbContext, IClock clock,
        IServiceProvider serviceProvider,
        IDomainEventsAccessor domainEventsAccessor)
    {
        _dbContext = dbContext;
        _clock = clock;
        _serviceProvider = serviceProvider;
        _domainEventsAccessor = domainEventsAccessor;
    }

    public async Task DispatchEventsAsync()
    {
        var domainEvents = _domainEventsAccessor.GetAllDomainEvents();
        var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();

        foreach (var domainEvent in domainEvents)
        {
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
            var handlers = _serviceProvider.GetServices(handlerType);

            var tasks = handlers.Select(x => (Task)handlerType
                .GetMethod(nameof(IDomainEventHandler<IDomainEvent>.HandleAsync))
                ?.Invoke(x, new object[] { domainEvent })
            );

            foreach (var task in tasks)
                if (task is not null)
                    await task.WaitAsync(new CancellationToken());
        }

        foreach (var domainEvent in domainEvents)
        {
            var domainEventType = typeof(IDomainEvent);
            var domainNotificationType = typeof(DomainNotificationBase<>).MakeGenericType(domainEventType);

            if (Activator.CreateInstance(domainNotificationType, domainEvent, domainEvent.Id) is
                IDomainEventNotification<IDomainEvent> domainNotificationInstance)
                domainEventNotifications.Add(domainNotificationInstance);
        }

        var outboxMessages = ConvertDomainEventNotificationsIntoOutboxMessages(domainEventNotifications);
        _dbContext.Set<OutboxMessage>().AddRange(outboxMessages);
    }

    private IEnumerable<OutboxMessage> ConvertDomainEventNotificationsIntoOutboxMessages(
        IEnumerable<IDomainEventNotification> domainEventNotifications)
    {
        return domainEventNotifications.Select(domainEventNotification => new OutboxMessage
        (
            Guid.NewGuid(),
            _clock.DateTimeOffsetNow(),
            domainEventNotification.GetType().Name,
            JsonConvert.SerializeObject(
                domainEventNotification,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                })
        )).ToList();
    }
}