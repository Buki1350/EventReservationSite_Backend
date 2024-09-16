using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Events.CreateEvent;

public record CreateEventCommand(Guid Id, string Title, string Description, DateTime StartDate, DateTime EndDate, string Location, int MaxNumberOfReservations) : ICommand;