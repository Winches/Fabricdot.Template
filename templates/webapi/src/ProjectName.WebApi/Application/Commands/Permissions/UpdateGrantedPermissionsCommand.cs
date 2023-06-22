using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Permissions;

/// <summary>
///     Update all permissions of subject
/// </summary>
public class UpdateGrantedPermissionsCommand : Command
{
    public string GrantType { get; }

    public string Subject { get; }

    public string[] Objects { get; }

    public UpdateGrantedPermissionsCommand(
        string grantType,
        string subject,
        string[] objects)
    {
        GrantType = Guard.Against.NullOrEmpty(grantType, nameof(grantType));
        Subject = Guard.Against.NullOrEmpty(subject, nameof(subject));
        Objects = Guard.Against.Null(objects, nameof(objects));
    }
}
