using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Users;

public class ChangePasswordCommand : Command
{
    public Guid UserId { get; }

    public ChangePasswordDto Data { get; }

    public ChangePasswordCommand(
        Guid userId,
        ChangePasswordDto data)
    {
        UserId = userId;
        Data = Guard.Against.Null(data, nameof(data));
    }
}
