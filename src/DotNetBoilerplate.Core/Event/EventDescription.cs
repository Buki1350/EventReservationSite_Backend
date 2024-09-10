namespace DotNetBoilerplate.Core.Event;

public sealed class EventDescription
{
    public EventDescription(string value)
    {
        if (value == null)
            Value = "No description provided";
        else
            Value = value;
    }
    
    public string Value { get; set; }
}