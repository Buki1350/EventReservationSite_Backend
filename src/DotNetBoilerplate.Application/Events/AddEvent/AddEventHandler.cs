using DotNetBoilerplate.Core.Event;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Time;

namespace DotNetBoilerplate.Application.Events.AddEvent;

internal sealed class AddEventHandler : ICommandHandler<AddEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public AddEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task HandleAsync(AddEventCommand command)
    {
        var eventId = new EventId(command.EventId);
        var organizerId = new EventOrganizerId(command.OrganizerId);
        var title = new EventTitle(command.Title);
        var description = new EventDescription(command.Description);
        var location = new EventLocation(command.Location);
        var eventDate = new EventDate(command.EventDate);
        var maxNumberOfTickets = new MaxNumberOfTickets(command.MaxNumberOfTickets);
        
        var newEvent = Event.New(eventId, organizerId, title, description, location, eventDate, maxNumberOfTickets);
        
        await _eventRepository.AddAsync(newEvent);
    }
}