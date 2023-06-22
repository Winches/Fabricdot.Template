using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;

namespace ProjectName.WebApi.Application.Commands.Users;

internal class ChangeUserStatusCommandHandler : CommandHandler<ChangeUserStatusCommand>
{
    private readonly UserManager<User> _userManager;

    public ChangeUserStatusCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public override async Task ExecuteAsync(
        ChangeUserStatusCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        Guard.Against.Null(user, nameof(user));

        if (command.IsActive)
            user.Enable();
        else
            user.Disable();
        var res = await _userManager.UpdateAsync(user);
        res.EnsureSuccess();
    }
}
