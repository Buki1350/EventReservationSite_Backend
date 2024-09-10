using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Events.AddEvent;

public record AddEventCommand(Guid EventId, Guid OrganizerId, string Title, string Description, string Location, DateTime EventDate, int MaxNumberOfTickets) : ICommand;