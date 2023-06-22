using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Queries;

namespace ProjectName.WebApi.Application.Queries.Permissions;

/// <summary>
///     List permission grant
/// </summary>
public class GetPermissionGrantsQuery : Query<ICollection<string>>
{
    public string GrantType { get; }

    public string Subject { get; }

    public GetPermissionGrantsQuery(
        string grantType,
        string subject)
    {
        GrantType = Guard.Against.NullOrEmpty(grantType, nameof(grantType));
        Subject = Guard.Against.NullOrEmpty(subject, nameof(subject));
    }
}
