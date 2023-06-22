using Fabricdot.Infrastructure.Queries;
using Fabricdot.WebApi.Models;

namespace ProjectName.WebApi.Application.Queries.Roles;

public class GetRolePagedListQuery : PageQueryBase<PagedResultDto<RoleDto>>
{
    /// <summary>
    ///     Name
    /// </summary>
    public string? Filter { get; set; }
}
