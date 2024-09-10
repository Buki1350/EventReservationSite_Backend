namespace DotNetBoilerplate.Core.Event;

public sealed record MaxNumberOfTickets
{
    public MaxNumberOfTickets(int value)
    {
        Value = value;
    }
    
    public MaxNumberOfTickets(){}
    
    public int Value { get; }
}