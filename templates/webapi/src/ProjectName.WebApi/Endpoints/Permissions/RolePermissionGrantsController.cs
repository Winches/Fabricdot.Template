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
///     Role permission grant
/// </summary>
[Route("api/role")]
public class RolePermissionGrantsController : PermissionGrantsController
{
    protected override string GrantType => GrantTypes.Role;

    /// <summary>
    ///     Grant permission to role
    /// </summary>
    /// <param name="role"></param>
    /// <param name="permission"></param>
    /// <returns></returns>
    [Description("grant permission to role")]
    [Authorize(ApplicationPermissions.Roles.ManagePermission)]
    [HttpPost("{role}/permission")]
    public async Task CreateAsync(
        [FromRoute] string role,
        [FromQuery][Required] string permission)
    {
        await CommandBus.PublishAsync(new GrantPermissionCommand(
            GrantType,
            role,
            permission));
    }

    /// <summary>
    ///     Revoke permission to role
    /// </summary>
    /// <param name="role"></param>
    /// <param name="permission"></param>
    /// <returns></returns>
    [Description("revoke permission to role")]
    [Authorize(ApplicationPermissions.Roles.ManagePermission)]
    [HttpDelete("{role}/permission")]
    public async Task DeleteAsync(
        [FromRoute] string role,
        [FromQuery][Required] string permission)
    {
        await CommandBus.PublishAsync(new RevokePermissionCommand(
            GrantType,
            role,
            permission));
    }

    /// <summary>
    ///     Update permissions to tole
    /// </summary>
    /// <param name="role"></param>
    /// <param name="permissions"></param>
    /// <returns></returns>
    [Description("update permissions to role")]
    [Authorize(ApplicationPermissions.Roles.ManagePermission)]
    [Consumes(MediaTypeNames.Application.Json)]
    [HttpPut("{role}/permission")]
    public virtual async Task UpdateAsync(
        [FromRoute] string role,
        [FromBody][Required] string[] permissions)
    {
        await CommandBus.PublishAsync(new UpdateGrantedPermissionsCommand(
            GrantType,
            role,
            permissions));
    }

    /// <summary>
    ///     List permissions of role
    /// </summary>
    /// <param name="role"></param>
    /// <returns></returns>
    [Description("list granted permissions of role")]
    [Authorize(ApplicationPermissions.Roles.ManagePermission)]
    [HttpGet("{role}/permission/list")]
    public async Task<ICollection<string>> ListAsync([FromRoute] string role)
    {
        return await QueryProcessor.ProcessAsync(new GetPermissionGrantsQuery(GrantType, role));
    }
}
