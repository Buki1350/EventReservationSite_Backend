using NSubstitute;
using Xunit;
using System;
using System.Threading.Tasks;
using DotNetBoilerplate.Application.Events.UpdateEvent;
using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Time;

public class EventTests
{
    private readonly IEventRepository _eventRepository;
    private readonly IContext _context;
    private readonly IClock _clock;
    private readonly UpdateEventHandler _updateEventHandler;

    public EventTests()
    {
        _eventRepository = Substitute.For<IEventRepository>();
        _context = Substitute.For<IContext>();
        _clock = Substitute.For<IClock>();
        _updateEventHandler = new UpdateEventHandler(_eventRepository, _clock, _context);
    }

    [Fact]
    public async Task Handle_Should_ThrowInvalidMaxNumberOfTicketsException_When_MaxReservations_IsNullOrLessThanOne()
    {
        // Arrange
        var eventId = new EventId(Guid.NewGuid());
        const int maxReservationsZero = 0;
        const int maxReservationsNull = 0;
        var command1 = new UpdateEventCommand(eventId, "SomeTitle", "SomeDescription", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "SomeLocation", maxReservationsZero);
        var command2 = new UpdateEventCommand(eventId, "SomeTitle", "SomeDescription", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "SomeLocation", maxReservationsNull);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidMaxNumberOfTicketsException>(() => _updateEventHandler.HandleAsync(command1));
        await Assert.ThrowsAsync<InvalidMaxNumberOfTicketsException>(() => _updateEventHandler.HandleAsync(command2));
    }

    [Fact]
    public async Task Handle_Should_ThrowEventNotFoundException_When_EventDoesNotExist()
    {
        // Arrange
        var eventId = new EventId(Guid.NewGuid());
        var command = new UpdateEventCommand(eventId, "SomeTitle", "SomeDescription", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "SomeLocation", 10);
        _eventRepository.FindByIdAsync(eventId).Returns(Task.FromResult<Event>(null));

        // Act & Assert
        await Assert.ThrowsAsync<EventNotFoundException>(() => _updateEventHandler.HandleAsync(command));
    }

    [Fact]
    public async Task Handle_Should_ThrowWrongUserIdentityException_When_UserIsNotTheOrganizer()
    {
        // Arrange
        var organizerId = Guid.NewGuid();
        var eventId = new EventId(Guid.NewGuid());
        var command = new UpdateEventCommand(eventId, "SomeTitle", "SomeDescription", DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), "SomeLocation", 10);
        var existingEvent = Event.Create(eventId, organizerId, "SomeTitle", "SomeDescription", new EventStartDate(DateTime.Now.AddDays(1), _clock.Now()), DateTime.Now.AddDays(2), "SomeLocation", 10);

        _eventRepository.FindByIdAsync(eventId).Returns(Task.FromResult(existingEvent));
        _context.Identity.Id.Returns(Guid.NewGuid()); // Simulating different user

        // Act & Assert
        await Assert.ThrowsAsync<WrongUserIdentityException>(() => _updateEventHandler.HandleAsync(command));
    }
}