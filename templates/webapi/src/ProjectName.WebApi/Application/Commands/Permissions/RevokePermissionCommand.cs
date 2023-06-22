using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Permissions;

/// <summary>
///     Revoke permission
/// </summary>
public class RevokePermissionCommand : Command
{
    public string GrantType { get; }

    public string Subject { get; }

    public string Object { get; }

    public RevokePermissionCommand(
        string grantType,
        string subject,
        string @object)
    {
        GrantType = Guard.Against.NullOrEmpty(grantType, nameof(grantType));
        Subject = Guard.Against.NullOrEmpty(subject, nameof(subject));
        Object = Guard.Against.NullOrEmpty(@object, nameof(@object));
    }
}
