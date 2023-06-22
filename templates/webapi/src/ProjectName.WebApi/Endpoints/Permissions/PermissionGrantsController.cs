using System.Net.Mime;
using Fabricdot.WebApi.Endpoint;
using Microsoft.AspNetCore.Mvc;
using ProjectName.WebApi.Authorization;

namespace ProjectName.WebApi.Endpoints.Permissions;

[DefaultAuthorize]
[Produces(MediaTypeNames.Application.Json)]
public abstract class PermissionGrantsController : EndPointBase
{
    protected abstract string GrantType { get; }
}
