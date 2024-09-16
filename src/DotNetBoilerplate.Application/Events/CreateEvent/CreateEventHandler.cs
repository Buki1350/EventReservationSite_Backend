using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;

namespace DotNetBoilerplate.Application.Events.CreateEvent;

internal sealed class CreateEventHandler : ICommandHandler<CreateEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IContext _context;

    public CreateEventHandler(IEventRepository eventRepository, IContext context)
    {
        _eventRepository = eventRepository;
        _context = context;
    }
    
    public async Task HandleAsync(CreateEventCommand command)
    {
        var id = new EventId(command.Id);
        var title = new EventTitle(command.Title);
        var description = new EventDescription(command.Description);
        var startDate = new EventStartDate(command.StartDate, DateTime.Now);
        var endDate = new EventEndDate(command.EndDate);
        var location = new EventLocation(command.Location);
        var maxNumberOfReservations = new EventMaxNumberOfReservations(command.MaxNumberOfReservations);
        
        var @event = Event.Create(id, _context.Identity.Id, title, description, startDate, endDate, location, maxNumberOfReservations);
        
        await _eventRepository.AddAsync(@event);
    }
}