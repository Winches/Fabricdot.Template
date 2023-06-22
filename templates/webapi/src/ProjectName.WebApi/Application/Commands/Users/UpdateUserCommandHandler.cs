using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;

namespace ProjectName.WebApi.Application.Commands.Users;

internal class UpdateUserCommandHandler : CommandHandler<UpdateUserCommand>
{
    private readonly UserManager<User> _userManager;

    public UpdateUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public override async Task ExecuteAsync(
        UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(command.UserId.ToString());
        Guard.Against.Null(user, nameof(user));

        user.GivenName = command.GivenName.Trim();
        user.Surname = command.Surname?.Trim();
        await _userManager.SetEmailAsync(user, command.Email);
        await _userManager.SetPhoneNumberAsync(user, command.PhoneNumber);
        var res = await _userManager.UpdateAsync(user);
        res.EnsureSuccess();
    }
}
