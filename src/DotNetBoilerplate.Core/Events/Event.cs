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
        EventId eventId,
        UserId organizerId,
        EventTitle title,
        EventDescription description,
        EventStartDate startDate,
        EventEndDate endDate,
        EventLocation location,
        EventMaxNumberOfReservations maxNumberOfReservations)
    {
        if (endDate is null) endDate = new EventEndDate(startDate.Value);
        else if (endDate.Value < startDate.Value) throw new InvalidEndDateException(startDate.Value, endDate.Value);
        
        var @event = new Event()
        {
            Id = eventId,
            OrganizerId = organizerId,
            Title = title,
            Description = description,
            StartDate = startDate,
            EndDate = endDate,
            Location = location,
            MaxNumberOfReservations = maxNumberOfReservations
        };
            
        return @event;
    }
    
    public EventId Id { get; private set; }
    public UserId OrganizerId { get; private set; }
    public EventTitle Title { get; private set; }
    public EventDescription Description { get; private set; }
    public EventStartDate StartDate { get; private set; }
    public EventEndDate EndDate { get; private set; }
    public EventLocation Location { get; private set; }
    public EventMaxNumberOfReservations MaxNumberOfReservations { get; private set; }
    public readonly List<Reservation> Reservations = [];

    public void Update(
        EventTitle newTitle,
        EventDescription newDescription,
        EventStartDate newStartDate,
        EventEndDate newEndDate,
        EventLocation newLocation,
        EventMaxNumberOfReservations newMaxNumberOfReservations
        )
    {
        Title = newTitle;
        Description = newDescription;
        StartDate = newStartDate;
        EndDate = newEndDate;
        Location = newLocation;
        MaxNumberOfReservations = newMaxNumberOfReservations;
    }
    
    public void MakeReservation(Users.UserId userId, EventId eventId, DateTime now)
    {
        if (now > StartDate.Value) throw new TooLateReservationTimeException();
        if (Reservations.Count + 1 > MaxNumberOfReservations) throw new InvalidNumberOfReservationsException(MaxNumberOfReservations.Value);
        
        Reservations.Add(Reservation.Create(userId, eventId, now));
    }

    public void CancelReservation(ReservationId reservationId)
    {
        var reservation = Reservations.FirstOrDefault(r => r.Id == reservationId);
        
        if (reservation is null) throw new ReservationNotFoundException(reservationId.Value);
        
        Reservations.Find(r => r.Id.Value == reservationId.Value).Cancel();
    }
}