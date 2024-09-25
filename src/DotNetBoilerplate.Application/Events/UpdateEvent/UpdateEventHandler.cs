using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Exceptions;
using DotNetBoilerplate.Shared.Abstractions.Time;

namespace DotNetBoilerplate.Application.Events.UpdateEvent;

public class WrongUserIdentityException() : CustomException("User want to change event that is not his");
internal sealed class UpdateEventHandler : ICommandHandler<UpdateEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IContext _context;

    public UpdateEventHandler(IEventRepository eventRepository, IContext context)
    {
        _eventRepository = eventRepository;
        _context = context;
    }

    public async Task HandleAsync(UpdateEventCommand command)
    {
        //UNIT TESTY
        var newMaxNumberOfReservations = new EventMaxNumberOfReservations(command.NewMaxNumberOfReservations);
        var newStartDate = new EventStartDate(command.NewStartDate);
        
        var @event = _eventRepository.FindByIdAsync(command.Id).Result;
        
        if (@event is null) throw new EventNotFoundException(command.Id);
        if (_context.Identity.Id != @event.OrganizerId.Value) throw new WrongUserIdentityException();
        
        
        @event.Update(command.NewTitle, command.NewDescription, newStartDate, command.NewEndDate, command.NewLocation, newMaxNumberOfReservations);
        
        await _eventRepository.UpdateAsync(@event);
    }
}