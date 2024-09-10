namespace DotNetBoilerplate.Shared.Abstractions.Domain;

public class DomainEventBase : IDomainEvent
{
    protected DomainEventBase()
    {
        Id = Guid.NewGuid();
        OccuredOn = DateTimeOffset.UtcNow;
    }

    public Guid Id { get; }
    public DateTimeOffset OccuredOn { get; }
}