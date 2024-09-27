namespace DotNetBoilerplate.Infrastructure.DAL.Configurations.Read.Model;

internal sealed class ReservationReadModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool Paid { get; set; }
    public bool Active { get; set; }
    public EventReadModel Event { get; set; }
}