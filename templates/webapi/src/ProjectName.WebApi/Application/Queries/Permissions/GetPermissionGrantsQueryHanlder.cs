using Fabricdot.Authorization;
using Fabricdot.Infrastructure.Queries;
using Fabricdot.PermissionGranting;

namespace ProjectName.WebApi.Application.Queries.Permissions;

internal class GetPermissionGrantsQueryHanlder : QueryHandler<GetPermissionGrantsQuery, ICollection<string>>
{
    private readonly IPermissionGrantingManager _permissionGrantingManager;

    public GetPermissionGrantsQueryHanlder(IPermissionGrantingManager permissionGrantingManager)
    {
        _permissionGrantingManager = permissionGrantingManager;
    }

    public override async Task<ICollection<string>> ExecuteAsync(
        GetPermissionGrantsQuery query,
        CancellationToken cancellationToken)
    {
        var list = await _permissionGrantingManager.ListAsync(
            new GrantSubject(query.GrantType, query.Subject),
            cancellationToken);

        return list.Select(v => v.Object).ToArray();
    }
}
