using System.Threading;
using System.Threading.Tasks;
using Fabricdot.Authorization;
using Fabricdot.Infrastructure.Commands;
using Fabricdot.PermissionGranting;
using MediatR;

namespace ProjectName.WebApi.Application.Commands.Permissions
{
    internal class GrantPermissionCommandHandler : ICommandHandler<GrantPermissionCommand>
    {
        private readonly IPermissionGrantingManager _permissionGrantingManager;

        public GrantPermissionCommandHandler(IPermissionGrantingManager permissionGrantingManager)
        {
            _permissionGrantingManager = permissionGrantingManager;
        }

        public virtual async Task<Unit> Handle(
            GrantPermissionCommand request,
            CancellationToken cancellationToken)
        {
            await _permissionGrantingManager.GrantAsync(
                new GrantSubject(request.GrantType, request.Subject),
                request.Object,
                cancellationToken);
            return Unit.Value;
        }
    }
}