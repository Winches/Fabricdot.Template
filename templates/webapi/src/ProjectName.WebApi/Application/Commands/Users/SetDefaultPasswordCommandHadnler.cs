using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;
using ProjectName.Domain.Shared.Constants;

namespace ProjectName.WebApi.Application.Commands.Users;

internal class SetDefaultPasswordCommandHadnler : CommandHandler<SetDefaultPasswordCommand>
{
    private readonly UserManager<User> _userManager;

    public SetDefaultPasswordCommandHadnler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public override async Task ExecuteAsync(
        SetDefaultPasswordCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        Guard.Against.Null(user, nameof(user));
        var res = await _userManager.RemovePasswordAsync(user);
        res.EnsureSuccess();
        res = await _userManager.AddPasswordAsync(user, UserConstants.DefaultPassword);
        res.EnsureSuccess();
    }
}
