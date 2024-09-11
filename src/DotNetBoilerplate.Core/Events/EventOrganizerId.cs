using DotNetBoilerplate.Core.CommonExceptions;

namespace DotNetBoilerplate.Core.Events;

public sealed record EventOrganizerId
{
    public EventOrganizerId(Guid value)
    {
        if (value == Guid.Empty) throw new InvalidEntityIdException(value);
        
        Value = value;
    }
    
    public EventOrganizerId(){}
    
    public Guid Value { get; }
    
    public static implicit operator Guid(EventOrganizerId value) => value.Value;
    public static implicit operator EventOrganizerId(Guid value) => new EventOrganizerId(value);
}