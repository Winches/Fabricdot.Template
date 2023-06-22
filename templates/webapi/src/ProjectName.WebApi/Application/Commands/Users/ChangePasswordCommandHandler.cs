using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;

namespace ProjectName.WebApi.Application.Commands.Users;

internal class ChangePasswordCommandHandler : CommandHandler<ChangePasswordCommand>
{
    private readonly UserManager<User> _userManager;

    public ChangePasswordCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public override async Task ExecuteAsync(
        ChangePasswordCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        Guard.Against.Null(user, nameof(user));

        var data = command.Data;
        var res = await _userManager.ChangePasswordAsync(
            user,
            data.CurrentPassword,
            data.NewPassword);
        res.EnsureSuccess();
    }
}
