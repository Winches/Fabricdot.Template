using System.Threading;
using System.Threading.Tasks;
using Fabricdot.Authorization;
using Fabricdot.Infrastructure.Commands;
using Fabricdot.PermissionGranting;
using MediatR;

namespace ProjectName.WebApi.Application.Commands.Permissions
{
    internal class UpdateGrantedPermissionsCommandHandler : ICommandHandler<UpdateGrantedPermissionsCommand>
    {
        private readonly IPermissionGrantingManager _permissionGrantingManager;

        public UpdateGrantedPermissionsCommandHandler(IPermissionGrantingManager permissionGrantingManager)
        {
            _permissionGrantingManager = permissionGrantingManager;
        }

        public async Task<Unit> Handle(
            UpdateGrantedPermissionsCommand request,
            CancellationToken cancellationToken)
        {
            await _permissionGrantingManager.SetAsync(
                new GrantSubject(request.GrantType, request.Subject),
                request.Objects,
                cancellationToken);

            return Unit.Value;
        }
    }
}