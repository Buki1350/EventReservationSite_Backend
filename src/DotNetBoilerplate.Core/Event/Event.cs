using DotNetBoilerplate.Shared.Abstractions.Domain;

namespace DotNetBoilerplate.Core.Event;

public class Event : Entity
{
    private Event(
        EventId eventId,
        EventOrganizerId organizerId,
        EventTitle title,
        EventDescription description,
        EventLocation location,
        EventDate eventDate,
        MaxNumberOfTickets maxNumberOfTickets)
    {
        Id = eventId;
        OrganizerId = organizerId;
        Title = title;
        Description = description;
        Location = location;
        EventDate = eventDate;
        MaxNumberOfTickets = maxNumberOfTickets;
    }

    private Event()
    {
    }
    
    public EventId Id { get; private set; }
    public EventOrganizerId OrganizerId { get; private set; }
    public EventTitle Title { get; private set; }
    public EventDescription Description { get; private set; }
    public EventLocation Location { get; private set; }
    public EventDate EventDate { get; private set; }
    public MaxNumberOfTickets MaxNumberOfTickets { get; private set; }

    public static Event New(EventId eventId, EventOrganizerId eventOrganizerId, EventTitle title, EventDescription description, EventLocation location,
        EventDate eventDate, MaxNumberOfTickets maxNumberOfTickets)
    {
        return new Event(eventId, eventOrganizerId, title, description, location, eventDate, maxNumberOfTickets);
    }
}