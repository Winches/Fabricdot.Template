using System;
using Fabricdot.Infrastructure.Queries;

namespace ProjectName.WebApi.Application.Queries.Users
{
    public class GetUserDetailsQuery : UserQueryBase, IQuery<UserDetailsDto>
    {
        public GetUserDetailsQuery(Guid userId) : base(userId)
        {
        }
    }
}