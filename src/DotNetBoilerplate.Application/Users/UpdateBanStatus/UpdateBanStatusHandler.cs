using DotNetBoilerplate.Core.Users;
using DotNetBoilerplate.Shared.Abstractions.Commands;
using DotNetBoilerplate.Shared.Abstractions.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Time;
using DotNetBoilerplate.Shared.Time;

namespace DotNetBoilerplate.Application.Users.UpdateBanStatus;

internal sealed class UpdateBanStatusHandler : ICommandHandler<UpdateBanStatsCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly IClock _clock;
    private readonly IContext _context;

    public UpdateBanStatusHandler(IUserRepository userRepository, IClock clock, IContext context)
    {
        _userRepository = userRepository;
        _clock = clock;
        _context = context;
    }

    public async Task HandleAsync(UpdateBanStatsCommand command)
    {
        if (_context.Identity.Role.Equals(Role.Admin))
            throw new UnauthorizedAccessException();

        var userId = command.UserId;
        var unban = command.IsBanned;
        
        var user = _userRepository.FindByIdAsync(userId).Result;
        
        user.UpdateIsBanned(unban, _clock.Now());
        
        await _userRepository.UpdateAsync(user);
    }
}