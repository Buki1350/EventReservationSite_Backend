using DotNetBoilerplate.Core.CommonExceptions;

namespace DotNetBoilerplate.Core.Events;

public sealed record EventId
{
    public EventId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidEntityIdException(value);

        Value = value;
    }
    
    public EventId(){}
    
    public Guid Value { get; }
    
    public static implicit operator Guid(EventId value) => value.Value;
    public static implicit operator EventId(Guid value) => new EventId(value);
    
}