using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Shared.Abstractions.Domain;
using DotNetBoilerplate.Shared.Abstractions.Exceptions;
using Xunit;
using System;

namespace DotNetBoilerplate.Tests.Unit
{
    public class EventTests
    {
        [Fact]
        public void Create_ShouldReturnEvent_WhenValidDataIsProvided()
        {
            // Arrange
            var organizerId = new EventOrganizerId(Guid.NewGuid());
            var title = new EventTitle("Test Event");
            var description = new EventDescription("This is a test event description.");
            var startDate = new EventStartDate(DateTime.Now.AddDays(1), DateTime.Now);
            var endDate = new EventEndDate(DateTime.Now.AddDays(2));
            var location = new EventLocation("Test Location");
            var maxNumberOfTickets = new EventMaxNumberOfTickets(100);

            // Act
            var @event = Event.Create(organizerId, title, description, startDate, endDate, location, maxNumberOfTickets);

            // Assert
            Assert.NotNull(@event);
            Assert.Equal(organizerId, @event.OrganizerId);
            Assert.Equal(title, @event.Title);
            Assert.Equal(description, @event.Description);
            Assert.Equal(startDate, @event.StartDate);
            Assert.Equal(endDate, @event.EndDate);
            Assert.Equal(location, @event.Location);
            Assert.Equal(maxNumberOfTickets, @event.MaxNumberOfTickets);
        }

        [Fact]
        public void Create_ShouldThrowInvalidEndDateException_WhenEndDateIsEarlierThanStartDate()
        {
            // Arrange
            var organizerId = new EventOrganizerId(Guid.NewGuid());
            var title = new EventTitle("Test Event");
            var description = new EventDescription("This is a test event description.");
            var startDate = new EventStartDate(DateTime.Now.AddDays(2), DateTime.Now);
            var endDate = new EventEndDate(DateTime.Now.AddDays(1));
            var location = new EventLocation("Test Location");
            var maxNumberOfTickets = new EventMaxNumberOfTickets(100);

            // Act & Assert
            var exception = Assert.Throws<InvalidEndDateException>(() => 
                Event.Create(organizerId, title, description, startDate, endDate, location, maxNumberOfTickets));

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
                new EventMaxNumberOfTickets(0));
            
            var exceptionLessThanZero = Assert.Throws<InvalidMaxNumberOfTicketsException>(() =>
                new EventMaxNumberOfTickets(-1));
            
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
    }
}
