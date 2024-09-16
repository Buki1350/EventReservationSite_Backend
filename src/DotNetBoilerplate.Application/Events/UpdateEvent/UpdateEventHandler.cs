using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Events.UpdateEvent;

internal sealed class UpdateEventHandler : ICommandHandler<UpdateEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public UpdateEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task HandleAsync(UpdateEventCommand command)
    {
        var eventId = command.Id;
        var newTitle = new EventTitle(command.NewTitle);
        var newDescription = new EventDescription(command.NewDescription);
        var newStartDate = new EventStartDate(command.NewStartDate, DateTime.Now);
        var newEndDate = new EventEndDate(command.NewEndDate);
        var newLocation = new EventLocation(command.NewLocation);
        var newMaxNumberOfReservations = new EventMaxNumberOfReservations(command.NewMaxNumberOfReservations);
        
        var @event = _eventRepository.FindByIdAsync(eventId).Result;
        
        @event.Update(newTitle, newDescription, newStartDate, newEndDate, newLocation, newMaxNumberOfReservations);
        
        await _eventRepository.UpdateAsync(@event);
    }
}