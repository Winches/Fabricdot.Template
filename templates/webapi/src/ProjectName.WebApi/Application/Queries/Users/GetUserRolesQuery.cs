using System;
using System.Collections.Generic;
using Fabricdot.Infrastructure.Queries;
using ProjectName.WebApi.Application.Queries.Roles;

namespace ProjectName.WebApi.Application.Queries.Users
{
    public class GetUserRolesQuery : UserQueryBase, IQuery<ICollection<RoleDto>>
    {
        public GetUserRolesQuery(Guid userId) : base(userId)
        {
        }
    }
}