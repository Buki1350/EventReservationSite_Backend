using DotNetBoilerplate.Core.Event;namespace DotNetBoilerplate.Core.Event;

public record EventOrganizerId
{
    public EventOrganizerId(Guid value)
    {
        Value = value;
    }
    
    public EventOrganizerId(){}
    
    public Guid Value { get; }
}