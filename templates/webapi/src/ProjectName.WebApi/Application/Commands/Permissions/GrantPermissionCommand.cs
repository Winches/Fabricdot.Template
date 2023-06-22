using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Permissions;

/// <summary>
///     Grant permission
/// </summary>
public class GrantPermissionCommand : Command
{
    public string GrantType { get; }

    public string Subject { get; }

    public string Object { get; }

    public GrantPermissionCommand(
        string grantType,
        string subject,
        string @object)
    {
        GrantType = Guard.Against.NullOrEmpty(grantType, nameof(grantType));
        Subject = Guard.Against.NullOrEmpty(subject, nameof(subject));
        Object = Guard.Against.NullOrEmpty(@object, nameof(@object));
    }
}
