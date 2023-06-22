using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;
using ProjectName.Domain.Shared.Constants;

namespace ProjectName.WebApi.Application.Commands.Users;

internal class LockoutUserCommandHandler : CommandHandler<LockoutUserCommand>
{
    private readonly UserManager<User> _userManager;

    public LockoutUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public override async Task ExecuteAsync(
        LockoutUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        Guard.Against.Null(user, nameof(user));

        var res = await _userManager.SetLockoutEnabledAsync(user, true);
        res.EnsureSuccess();
        var lockoutEnd = command.LockoutEnd ?? DateTimeOffset.UtcNow.AddDays(UserConstants.DefaultLockDays);
        res = await _userManager.SetLockoutEndDateAsync(user, lockoutEnd);
        res.EnsureSuccess();
    }
}
