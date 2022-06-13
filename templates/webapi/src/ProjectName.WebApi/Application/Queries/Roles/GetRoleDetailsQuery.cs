using System;
using Fabricdot.Infrastructure.Queries;

namespace ProjectName.WebApi.Application.Queries.Roles
{
    public class GetRoleDetailsQuery : IQuery<RoleDetailsDto>
    {
        public Guid RoleId { get; }

        public GetRoleDetailsQuery(Guid roleId)
        {
            RoleId = roleId;
        }
    }
}