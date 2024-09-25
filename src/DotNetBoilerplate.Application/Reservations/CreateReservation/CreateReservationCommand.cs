using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Reservations.CreateReservation;

public record CreateReservationCommand(Guid EventId) : ICommand;