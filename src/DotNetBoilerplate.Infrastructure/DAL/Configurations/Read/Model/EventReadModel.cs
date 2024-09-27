
using DotNetBoilerplate.Core.Reservations;

namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class EventReadModel
{
    public Guid Id { get; set; }
    public Guid OrganizerId { get; set; }
    public UserReadModel Organizer { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Location { get; set; }
    public int MaxNumberOfReservations { get; set; }
    public List<ReservationReadModel> Reservations { get; set; }
    
}