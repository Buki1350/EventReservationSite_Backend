using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Core.Reservations;
using DotNetBoilerplate.Core.Users;
using Xunit;

namespace DotNetBoilerplate.Tests.Unit.Core
{
    public class EventTests
    {
        Event MakeNewEvent(DateTime startTime, DateTime endTime, DateTime now, int maxNumberOfTickets = 100)
        {
            return Event.Create(
                new EventId(Guid.NewGuid()),
                new EventOrganizerId(Guid.NewGuid()),
                new EventTitle("Test Event"),
                new EventDescription("This is a test event description."),
                new EventStartDate(startTime, now),
                new EventEndDate(endTime),
                new EventLocation("Test Location"),
                new EventMaxNumberOfReservations(maxNumberOfTickets)
            );
        }
        
        [Fact]
        public void Create_ShouldReturnEvent_WhenValidDataIsProvided()
        {
            // Arrange
            var eventId = new EventId(Guid.NewGuid());
            var organizerId = new EventOrganizerId(Guid.NewGuid());
            var title = new EventTitle("Test Event");
            var description = new EventDescription("This is a test event description.");
            var startDate = new EventStartDate(DateTime.Now.AddDays(1), DateTime.Now);
            var endDate = new EventEndDate(DateTime.Now.AddDays(2));
            var location = new EventLocation("Test Location");
            var maxNumberOfTickets = new EventMaxNumberOfReservations(100);

            // Act
            var @event = Event.Create(eventId, organizerId, title, description, startDate, endDate, location, maxNumberOfTickets);

            // Assert
            Assert.NotNull(@event);
            Assert.Equal(organizerId, @event.OrganizerId);
            Assert.Equal(title, @event.Title);
            Assert.Equal(description, @event.Description);
            Assert.Equal(startDate, @event.StartDate);
            Assert.Equal(endDate, @event.EndDate);
            Assert.Equal(location, @event.Location);
            Assert.Equal(maxNumberOfTickets, @event.MaxNumberOfReservations);
        }

        [Fact]
        public void Create_ShouldThrowInvalidEndDateException_WhenEndDateIsEarlierThanStartDate()
        {
            // Arrange
            var eventId = new EventId(Guid.NewGuid());
            var organizerId = new EventOrganizerId(Guid.NewGuid());
            var title = new EventTitle("Test Event");
            var description = new EventDescription("This is a test event description.");
            var startDate = new EventStartDate(DateTime.Now.AddDays(2), DateTime.Now);
            var endDate = new EventEndDate(DateTime.Now.AddDays(1));
            var location = new EventLocation("Test Location");
            var maxNumberOfTickets = new EventMaxNumberOfReservations(100);

            // Act & Assert
            var exception = Assert.Throws<InvalidEndDateException>(() => 
                Event.Create(eventId, organizerId, title, description, startDate, endDate, location, maxNumberOfTickets));

            Assert.Equal($"End date ({endDate.Value}) cannot be earlier than start date({startDate.Value}).", exception.Message);
        }
        
        [Fact]
        public void EventStartDate_ShouldThrowInvalidEventStartDateException_WhenStartDateAlreadyPassed()
        {
            // Arrange
            DateTime startDate = DateTime.Now.AddDays(-2);
            
            // Act & Assert
            var exception = Assert.Throws<InvalidEventStartDateException>(() =>
                new EventStartDate(startDate, DateTime.Now));
            
            Assert.Equal($"Incorrect event starting date ({startDate}). Date has to be at least {EventStartDate.EventSpareTime} hours in advance.", exception.Message);
        }

        [Fact]
        public void EventMaxNumberOfTickets_ShouldThrowInvalidMaxNumberOfTicketsException_WhenGivenMaxNumberOfTicketsIsZeroOrLess()
        {      
            // Arrange & Act & Assert
            var exceptionZero = Assert.Throws<InvalidMaxNumberOfTicketsException>(() =>
                new EventMaxNumberOfReservations(0));
            
            var exceptionLessThanZero = Assert.Throws<InvalidMaxNumberOfTicketsException>(() =>
                new EventMaxNumberOfReservations(-1));
            
            Assert.Equal("Incorrect maximum number of tickets: 0", exceptionZero.Message);
            Assert.Equal("Incorrect maximum number of tickets: -1", exceptionLessThanZero.Message);
        }

        [Fact]
        public void EventLocation_ShouldThrowInvalidLocationException_WhenGivenLocationIsNull()
        {
            // Arrange
            string location_null = null;
            string location_empty = string.Empty;
            
            // Act & Assert
            var exception_null = Assert.Throws<InvalidEventLocationException>(() => new EventLocation(location_null));
            var exception_empty = Assert.Throws<InvalidEventLocationException>(() => new EventLocation(location_empty));
            
            Assert.Equal("The event location is null or empty.", exception_null.Message);
            Assert.Equal("The event location is null or empty.", exception_empty.Message);
        }

        [Fact]
        public void MakeReservation_ShouldThrowTooLateReservationException_WhenGivenDateIsLaterThanStartDate()
        {
            // Arrange
            var startDate = DateTime.Now.AddDays(-1);
            var endDate = DateTime.Now.AddDays(1);
            var now = DateTime.Now.AddDays(-2);
            var @event = MakeNewEvent(startDate, endDate, now);

            // Act & Assert
            var exception = Assert.Throws<TooLateReservationTimeException>(() =>
                @event.MakeReservation(new UserId(Guid.NewGuid()), new EventId(Guid.NewGuid()), DateTime.Now));

            Assert.Equal("Reservation cannot be done after event started.", exception.Message);
        }

        [Fact]
        public void MakeReservation_ShouldThrowInvalidNumberOfReservationsException_WhenReservationsAreFull()
        {
            // Arrange
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(2);
            var now = DateTime.Now;
            const int maxNumberOfReservations = 2;
            var @event = MakeNewEvent(startDate, endDate, now, maxNumberOfReservations);

            @event.MakeReservation(new UserId(Guid.NewGuid()), new EventId(Guid.NewGuid()), DateTime.Now);
            @event.MakeReservation(new UserId(Guid.NewGuid()), new EventId(Guid.NewGuid()), DateTime.Now);

            // Act & Assert
            var exception = Assert.Throws<InvalidNumberOfReservationsException>(() =>
                @event.MakeReservation(new UserId(Guid.NewGuid()), new EventId(Guid.NewGuid()), DateTime.Now));

            Assert.Equal($"Too many reservations - cannot be more than {maxNumberOfReservations}.", exception.Message);
        }

        [Fact]
        public void CancelReservation_ShouldThrowReservationNotFoundException_WhenReservationIsNotFound()
        {
            // Arrange
            var startDate = DateTime.Now.AddDays(1);
            var endDate = DateTime.Now.AddDays(2);
            var now = DateTime.Now;
            var @event = MakeNewEvent(startDate, endDate, now);
            var nonExistentReservationId = new ReservationId(Guid.NewGuid());

            // Act & Assert
            var exception = Assert.Throws<ReservationNotFoundException>(() =>
                @event.CancelReservation(nonExistentReservationId));

            Assert.Equal($"The reservation with the given address {nonExistentReservationId.Value} was not found.", exception.Message);
        }
    }
    
    
}
