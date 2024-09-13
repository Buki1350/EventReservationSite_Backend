using DotNetBoilerplate.Core.Events;
using DotNetBoilerplate.Core.Reservations;
using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Shared.Abstractions.Domain;
using DotNetBoilerplate.Shared.Abstractions.Exceptions;

namespace DotNetBoilerplate.Core.Events;

public class InvalidEndDateException(DateTime startValue, DateTime endValue) : CustomException($"End date ({endValue}) cannot be earlier than start date({startValue}).");
public class InvalidNumberOfReservationsException(int maxNumber) : CustomException($"Too many reservations - cannot be more than {maxNumber}.");
public class TooLateReservationTimeException() : CustomException($"Reservation cannot be done after event started.");
public class ReservationNotFoundException(Guid value) : CustomException($"The reservation with the given address {value} was not found.");

public class Event : Entity
{
    private Event(){}

    public static Event Create(
        EventOrganizerId organizerId,
        EventTitle title,
        EventDescription description,
        EventStartDate startDate,
        EventEndDate endDate,
        EventLocation location,
        EventMaxNumberOfTickets maxNumberOfTickets)
    {
        if (endDate is null) endDate = new EventEndDate(startDate.Value);
        else if (endDate.Value < startDate.Value) throw new InvalidEndDateException(startDate.Value, endDate.Value);
        
        var @event = new Event()
        {
            Id = Guid.NewGuid(),
            OrganizerId = organizerId,
            Title = title,
            Description = description,
            StartDate = startDate,
            EndDate = endDate,
            Location = location,
            MaxNumberOfTickets = maxNumberOfTickets
        };
            
        return @event;
    }
    
    public Guid Id { get; private set; }
    public EventOrganizerId OrganizerId { get; private set; }
    public EventTitle Title { get; private set; }
    public EventDescription Description { get; private set; }
    public EventStartDate StartDate { get; private set; }
    public EventEndDate EndDate { get; private set; }
    public EventLocation Location { get; private set; }
    public EventMaxNumberOfTickets MaxNumberOfTickets { get; private set; }
    public readonly List<Reservation> Reservations = [];

    public void Update(Event @event)
    {
        Description = @event.Description;
        StartDate = @event.StartDate;
        EndDate = @event.EndDate;
        Location = @event.Location;
        MaxNumberOfTickets = @event.MaxNumberOfTickets;
    }
    
    public void MakeReservation(UserId userId, EventId eventId, DateTime now) //unity
    {
        if (now > StartDate.Value) throw new TooLateReservationTimeException();
        if (Reservations.Count + 1 > MaxNumberOfTickets) throw new InvalidNumberOfReservationsException(MaxNumberOfTickets.Value);
        
        Reservations.Add(Reservation.Create(userId, eventId, now));
    }

    public void CancelReservation(ReservationId reservationId) //unity
    {
        var reservation = Reservations.FirstOrDefault(r => r.Id == reservationId);
        
        if (reservation is null) throw new ReservationNotFoundException(reservationId.Value);
        
        Reservations.Find(r => r.Id.Value == reservationId.Value).Cancel();
    }
}