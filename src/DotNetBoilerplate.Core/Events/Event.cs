using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Shared.Abstractions.Domain;
using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Events;

public class InvalidEndDateException(DateTime startValue, DateTime endValue) : CustomException($"End date ({endValue}) cannot be earlier than start date({startValue}).");

public class Event : Entity
{
    private Event(){}

    public static Event Create(
        EventOrganizerId organizerId,
        EventTitle title,
        EventDescription description,
        EventStartDate startDate,
        EventEndDate endDate,
        EventLocation location,
        EventMaxNumberOfTickets maxNumberOfTickets)
    {
        if (endDate is null) endDate = new EventEndDate(startDate.Value);
        else if (endDate.Value < startDate.Value) throw new InvalidEndDateException(startDate.Value, endDate.Value);
        
        var @event = new Event()
        {
            Id = Guid.NewGuid(),
            OrganizerId = organizerId,
            Title = title,
            Description = description,
            StartDate = startDate,
            EndDate = endDate,
            Location = location,
            MaxNumberOfTickets = maxNumberOfTickets
        };
            
        return @event;
    }
    
    public Guid Id { get; private set; }
    public EventOrganizerId OrganizerId { get; private set; }
    public EventTitle Title { get; private set; }
    public EventDescription Description { get; private set; }
    public EventStartDate StartDate { get; private set; }
    public EventEndDate EndDate { get; private set; }
    public EventLocation Location { get; private set; }
    public EventMaxNumberOfTickets MaxNumberOfTickets { get; private set; }
    
}