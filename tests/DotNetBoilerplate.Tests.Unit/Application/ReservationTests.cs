using DotNetBoilerplate.Application.Exceptions;
using DotNetBoilerplate.Application.Reservations.CreateReservation;
using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Time;
using NSubstitute;
using Xunit;

public class ReservationTests
{
    private readonly IEventRepository _eventRepository;
    private readonly IContext _context;
    private readonly IClock _clock;
    private readonly CreateReservationHandler _createReservationHandler;

    public ReservationTests()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _context = Substitute.For<IContext>();
        _clock = Substitute.For<IClock>();
        _createReservationHandler = new CreateReservationHandler(_eventRepository, _context, _clock);
    }

    [Fact]
    public async Task HandleAsync_Should_ThrowEventNotFoundException_When_EventDoesNotExist()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var command = new CreateReservationCommand(eventId);
        
        _context.Identity.Id.Returns(userId);
        _eventRepository.FindByIdAsync(eventId).Returns(Task.FromResult<Event>(null));
        
        // Act & Assert
        await Assert.ThrowsAsync<EventNotFoundException>(() => _createReservationHandler.HandleAsync(command));
    }

    [Fact]
    public async Task HandleAsync_Should_CreateReservation_When_EventExists()
    {
        // Arrange
        var eventId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var command = new CreateReservationCommand(eventId);
        var now = DateTime.UtcNow;
        
        _context.Identity.Id.Returns(userId);
        _clock.Now().Returns(now);
        
        var existingEvent = Event.Create(eventId, userId, "Some Title", "Some Description", new EventStartDate(now.AddDays(1)), now.AddDays(2), "Some Location", 100);
        _eventRepository.FindByIdAsync(eventId).Returns(Task.FromResult(existingEvent));

        // Act
        await _createReservationHandler.HandleAsync(command);

        // Assert
        await _eventRepository.Received(1).UpdateAsync(Arg.Is<Event>(e => 
            e.Id.Equals(eventId) &&
            e.Reservations.Count == 1 
        ));
    }
}
