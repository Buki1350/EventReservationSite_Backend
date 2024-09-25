using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Core.Reservations;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Time;

namespace DotNetBoilerplate.Application.Reservations.CreateReservation;

internal sealed class CreateReservationHandler : ICommandHandler<CreateReservationCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IContext _context;
    private readonly IClock _clock;

    public CreateReservationHandler(IEventRepository eventRepository, IContext context, IClock clock)
    {
        _eventRepository = eventRepository;
        _context = context;
        _clock = clock;
    }

    public async Task HandleAsync(CreateReservationCommand command)
    {
        var reservation = Reservation.Create(command.EventId, _context.Identity.Id, _clock.Now());
        var @event = _eventRepository.FindByIdAsync(command.EventId).Result;
        if (@event is null) throw new EventNotFoundException(command.EventId);
        @event.MakeReservation(reservation);
        
        await _eventRepository.UpdateAsync(@event);
    }
}