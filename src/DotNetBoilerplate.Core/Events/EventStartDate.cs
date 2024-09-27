using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Events;

public class InvalidEventStartDateException(DateTime value) : CustomException($"Incorrect event starting date ({value}). Date has to be at least {EventStartDate.EventSpareTime} hours in advance.");
public sealed record EventStartDate
{
    public EventStartDate() {}
    public EventStartDate(DateTime value)
    {
        DateTime earliestStartDate = DateTime.Now.AddHours(EventSpareTime);
        // start at least 12 hours in advance
        if (value <= earliestStartDate) throw new InvalidEventStartDateException(value);
        
        Value = value;
    }
    
    public static int EventSpareTime { get; } = 12;
    public DateTime Value { get; init; }
    public static implicit operator DateTime(EventStartDate value) => value.Value;
    public static implicit operator EventStartDate(DateTime value) => new EventStartDate(value);
}