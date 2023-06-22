using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;

namespace ProjectName.WebApi.Application.Commands.Users;

internal class RemoveUserRolesCommandHandler : CommandHandler<RemoveUserRolesCommand>
{
    private readonly UserManager<User> _userManager;

    public RemoveUserRolesCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public override async Task ExecuteAsync(
        RemoveUserRolesCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        Guard.Against.Null(user, nameof(user));

        var res = await _userManager.RemoveFromRolesAsync(user, command.RoleNames);
        res.EnsureSuccess();
    }
}
