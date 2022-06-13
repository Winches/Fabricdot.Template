using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fabricdot.Authorization;
using Fabricdot.Infrastructure.Queries;
using Fabricdot.PermissionGranting;

namespace ProjectName.WebApi.Application.Queries.Permissions
{
    internal class GetPermissionGrantsQueryHanlder : IQueryHandler<GetPermissionGrantsQuery, ICollection<string>>
    {
        private readonly IPermissionGrantingManager _permissionGrantingManager;

        public GetPermissionGrantsQueryHanlder(IPermissionGrantingManager permissionGrantingManager)
        {
            _permissionGrantingManager = permissionGrantingManager;
        }

        public async Task<ICollection<string>> Handle(
            GetPermissionGrantsQuery request,
            CancellationToken cancellationToken)
        {
            var list = await _permissionGrantingManager.ListAsync(
                new GrantSubject(request.GrantType, request.Subject),
                cancellationToken);

            return list.Select(v => v.Object).ToArray();
        }
    }
}