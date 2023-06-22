using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Users;

public class ChangeUserStatusCommand : Command
{
    public Guid UserId { get; }

    public bool IsActive { get; }

    public ChangeUserStatusCommand(
        Guid userId,
        bool isActive)
    {
        UserId = userId;
        IsActive = isActive;
    }
}
