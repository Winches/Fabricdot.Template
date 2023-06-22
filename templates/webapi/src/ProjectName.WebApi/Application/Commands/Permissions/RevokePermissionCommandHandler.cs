using Fabricdot.Authorization;
using Fabricdot.Infrastructure.Commands;
using Fabricdot.PermissionGranting;

namespace ProjectName.WebApi.Application.Commands.Permissions;

internal class RevokePermissionCommandHandler : CommandHandler<RevokePermissionCommand>
{
    private readonly IPermissionGrantingManager _permissionGrantingManager;

    public RevokePermissionCommandHandler(IPermissionGrantingManager permissionGrantingManager)
    {
        _permissionGrantingManager = permissionGrantingManager;
    }

    public override async Task ExecuteAsync(
        RevokePermissionCommand command,
        CancellationToken cancellationToken)
    {
        await _permissionGrantingManager.RevokeAsync(
            new GrantSubject(command.GrantType, command.Subject),
            command.Object,
            cancellationToken);
    }
}
