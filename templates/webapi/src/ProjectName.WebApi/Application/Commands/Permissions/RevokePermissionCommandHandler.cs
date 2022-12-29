using System.Threading;
using System.Threading.Tasks;
using Fabricdot.Authorization;
using Fabricdot.Infrastructure.Commands;
using Fabricdot.PermissionGranting;
using MediatR;

namespace ProjectName.WebApi.Application.Commands.Permissions
{
    internal class RevokePermissionCommandHandler : CommandHandler<RevokePermissionCommand>
    {
        private readonly IPermissionGrantingManager _permissionGrantingManager;

        public RevokePermissionCommandHandler(IPermissionGrantingManager permissionGrantingManager)
        {
            _permissionGrantingManager = permissionGrantingManager;
        }

        public override async Task<Unit> ExecuteAsync(
            RevokePermissionCommand command,
            CancellationToken cancellationToken)
        {
            await _permissionGrantingManager.RevokeAsync(
                new GrantSubject(command.GrantType, command.Subject),
                command.Object,
                cancellationToken);
            return Unit.Value;
        }
    }
}