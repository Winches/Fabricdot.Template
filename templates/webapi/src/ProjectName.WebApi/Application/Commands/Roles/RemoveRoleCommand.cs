using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Roles;

public class RemoveRoleCommand : Command
{
    public Guid RoleId { get; }

    public RemoveRoleCommand(Guid roleId)
    {
        RoleId = roleId;
    }
}
