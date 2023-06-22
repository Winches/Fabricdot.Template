using Fabricdot.Infrastructure.Queries;
using Fabricdot.WebApi.Models;

namespace ProjectName.WebApi.Application.Queries.Users;

public class GetUserPagedListQuery : PageQueryBase<PagedResultDto<UserDetailsDto>>
{
    /// <summary>
    ///     UserName/GivenName/Email/PhoneNumber
    /// </summary>
    public string? Filter { get; set; }

    public bool? IsLockedOut { get; set; }

    public bool? IsActive { get; set; }
}
