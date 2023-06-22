using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;
using Fabricdot.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Infrastructure.Security;
using ProjectName.WebApi.Application.Commands.Permissions;
using ProjectName.WebApi.Application.Queries.Permissions;

namespace ProjectName.WebApi.Endpoints.Permissions;

/// <summary>
///     User permission grant
/// </summary>
[Produces(MediaTypeNames.Application.Json)]
[Route("api/user")]
public class UserPermissionGrantsController : PermissionGrantsController
{
    protected override string GrantType => GrantTypes.User;

    /// <summary>
    ///     Grant permission to user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="permission"></param>
    /// <returns></returns>
    [Description("grant permission to user")]
    [Authorize(ApplicationPermissions.Users.ManagePermission)]
    [Consumes(MediaTypeNames.Application.Json)]
    [HttpPost("{id}/permission")]
    public virtual async Task CreateAsync(
        [FromRoute] Guid id,
        [FromBody][Required] string permission)
    {
        await CommandBus.PublishAsync(new GrantPermissionCommand(
            GrantType,
            id.ToString(),
            permission));
    }

    /// <summary>
    ///     Revoke permission to user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="permission"></param>
    /// <returns></returns>
    [Description("revoke permission to user")]
    [Authorize(ApplicationPermissions.Users.ManagePermission)]
    [Consumes(MediaTypeNames.Application.Json)]
    [HttpDelete("{id}/permission")]
    public virtual async Task DeleteAsync(
        [FromRoute] Guid id,
        [FromBody][Required] string permission)
    {
        await CommandBus.PublishAsync(new RevokePermissionCommand(
            GrantType,
            id.ToString(),
            permission));
    }

    /// <summary>
    ///     Update permissions to user
    /// </summary>
    /// <param name="id"></param>
    /// <param name="permissions"></param>
    /// <returns></returns>
    [Description("update permission to user")]
    [Authorize(ApplicationPermissions.Users.ManagePermission)]
    [Consumes(MediaTypeNames.Application.Json)]
    [HttpPut("{id}/permission")]
    public virtual async Task UpdateAsync(
        [FromRoute] Guid id,
        [FromBody][Required] string[] permissions)
    {
        await CommandBus.PublishAsync(new UpdateGrantedPermissionsCommand(
            GrantType,
            id.ToString(),
            permissions));
    }

    /// <summary>
    ///     List permissions of user
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Description("list granted permissions of user")]
    [Authorize(ApplicationPermissions.Users.ManagePermission)]
    [HttpGet("{id}/permission/list")]
    public async Task<ICollection<string>> ListAsync([FromRoute] Guid id)
    {
        return await QueryProcessor.ProcessAsync(new GetPermissionGrantsQuery(GrantType, id.ToString()));
    }
}
