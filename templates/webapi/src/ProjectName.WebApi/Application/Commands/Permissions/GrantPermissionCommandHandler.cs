using Fabricdot.Authorization;
using Fabricdot.Infrastructure.Commands;
using Fabricdot.PermissionGranting;

namespace ProjectName.WebApi.Application.Commands.Permissions;

internal class GrantPermissionCommandHandler : CommandHandler<GrantPermissionCommand>
{
    private readonly IPermissionGrantingManager _permissionGrantingManager;

    public GrantPermissionCommandHandler(IPermissionGrantingManager permissionGrantingManager)
    {
        _permissionGrantingManager = permissionGrantingManager;
    }

    public override async Task ExecuteAsync(
        GrantPermissionCommand command,
        CancellationToken cancellationToken)
    {
        await _permissionGrantingManager.GrantAsync(
            new GrantSubject(command.GrantType, command.Subject),
            command.Object,
            cancellationToken);
    }
}
