using ProjectName.WebApi.Application.Queries.Roles;

namespace ProjectName.WebApi.Application.Queries.Users;

public class GetUserRolesQuery : UserQueryBase<ICollection<RoleDto>>
{
    public GetUserRolesQuery(Guid userId) : base(userId)
    {
    }
}
