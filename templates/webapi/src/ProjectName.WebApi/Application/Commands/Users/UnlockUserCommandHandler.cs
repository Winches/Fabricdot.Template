using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;

namespace ProjectName.WebApi.Application.Commands.Users;

internal class UnlockUserCommandHandler : CommandHandler<UnlockUserCommand>
{
    private readonly UserManager<User> _userManager;

    public UnlockUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public override async Task ExecuteAsync(
        UnlockUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        Guard.Against.Null(user, nameof(user));

        if (user.IsLockedOut)
        {
            var res = await _userManager.SetLockoutEnabledAsync(user, true);
            res.EnsureSuccess();
            res = await _userManager.SetLockoutEndDateAsync(user, null);
            res.EnsureSuccess();
        }
    }
}
