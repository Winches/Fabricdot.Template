using Fabricdot.WebApi.Endpoint;
using ProjectName.WebApi.Authorization;

namespace ProjectName.WebApi.Endpoints.Permissions;

[DefaultAuthorize]
public abstract class PermissionGrantsController : EndPointBase
{
    protected abstract string GrantType { get; }
}
