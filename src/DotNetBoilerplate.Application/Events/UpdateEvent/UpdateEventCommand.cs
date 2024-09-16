using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Events.UpdateEvent;

public record UpdateEventCommand(Guid Id, string NewTitle, string NewDescription, DateTime NewStartDate, DateTime NewEndDate, string NewLocation, int NewMaxNumberOfReservations) : ICommand;