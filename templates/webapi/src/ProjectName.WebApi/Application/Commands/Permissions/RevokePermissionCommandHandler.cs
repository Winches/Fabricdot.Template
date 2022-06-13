using System.Threading;
using System.Threading.Tasks;
using Fabricdot.Authorization;
using Fabricdot.Infrastructure.Commands;
using Fabricdot.PermissionGranting;
using MediatR;

namespace ProjectName.WebApi.Application.Commands.Permissions
{
    internal class RevokePermissionCommandHandler : ICommandHandler<RevokePermissionCommand>
    {
        private readonly IPermissionGrantingManager _permissionGrantingManager;

        public RevokePermissionCommandHandler(IPermissionGrantingManager permissionGrantingManager)
        {
            _permissionGrantingManager = permissionGrantingManager;
        }

        public async Task<Unit> Handle(
            RevokePermissionCommand request,
            CancellationToken cancellationToken)
        {
            await _permissionGrantingManager.RevokeAsync(
                new GrantSubject(request.GrantType, request.Subject),
                request.Object,
                cancellationToken);
            return Unit.Value;
        }
    }
}