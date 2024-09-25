using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Time;

namespace DotNetBoilerplate.Application.Events.CreateEvent;

internal sealed class CreateEventHandler : ICommandHandler<CreateEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IContext _context;
    private readonly IClock _clock;
    
    public CreateEventHandler(IEventRepository eventRepository, IContext context, IClock clock)
    {
        _eventRepository = eventRepository;
        _context = context;
        _clock = clock;
    }
    
    public async Task HandleAsync(CreateEventCommand command)
    {
        var startDate = new EventStartDate(command.StartDate, _clock.Now());
        
        var @event = Event.Create(command.Id, _context.Identity.Id, command.Title, command.Description, startDate, command.EndDate, command.Location, command.MaxNumberOfReservations);
        
        await _eventRepository.AddAsync(@event);
    }
}