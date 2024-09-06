using DotNetBoilerplate.Shared.Abstractions.Commands;

namespace DotNetBoilerplate.Application.Users.UpdateBanStatus;

public record UpdateBanStatsCommand(Guid UserId, bool IsBanned) : ICommand;