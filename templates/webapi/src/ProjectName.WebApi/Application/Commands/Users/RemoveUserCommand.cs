using System;
using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Users
{
    public class RemoveUserCommand : CommandBase
    {
        public Guid UserId { get; }

        public RemoveUserCommand(Guid userId)
        {
            UserId = userId;
        }
    }
}