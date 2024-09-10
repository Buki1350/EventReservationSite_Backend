namespace DotNetBoilerplate.Shared.Abstractions.Outbox;

public class OutboxMessage
{
    public OutboxMessage(Guid id, DateTimeOffset occurredOn, string type, string data)
    {
        Id = id;
        OccurredOn = occurredOn;
        Type = type;
        Data = data;
    }

    public Guid Id { get; set; }
    public DateTimeOffset OccurredOn { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
    public DateTimeOffset? ProcessedDate { get; set; }
}