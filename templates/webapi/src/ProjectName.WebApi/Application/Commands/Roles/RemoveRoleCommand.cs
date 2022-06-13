using System;
using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Roles
{
    public class RemoveRoleCommand : CommandBase
    {
        public Guid RoleId { get; }

        public RemoveRoleCommand(Guid roleId)
        {
            RoleId = roleId;
        }
    }
}