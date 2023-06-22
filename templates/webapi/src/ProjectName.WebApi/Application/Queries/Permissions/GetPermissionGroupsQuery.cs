using Fabricdot.Authorization.Permissions;
using Fabricdot.Infrastructure.Queries;

namespace ProjectName.WebApi.Application.Queries.Permissions;

/// <summary>
///     List groups of permission
/// </summary>
public class GetPermissionGroupsQuery : Query<ICollection<PermissionGroup>>
{
}
