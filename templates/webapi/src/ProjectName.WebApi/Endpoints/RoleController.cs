using System.ComponentModel;
using System.Net.Mime;
using Fabricdot.WebApi.Endpoint;
using Fabricdot.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Infrastructure.Security;
using ProjectName.WebApi.Application.Commands.Roles;
using ProjectName.WebApi.Application.Queries.Roles;
using ProjectName.WebApi.Authorization;

namespace ProjectName.WebApi.Endpoints;

[DefaultAuthorize]
[Produces(MediaTypeNames.Application.Json)]
public class RoleController : EndPointBase
{
    /// <summary>
    ///     Create
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Description("create role")]
    [Authorize(ApplicationPermissions.Roles.Create)]
    [Consumes(MediaTypeNames.Application.Json)]
    [HttpPost]
    public async Task<Guid> CreateAsync([FromBody] CreateRoleCommand command)
    {
        return await CommandBus.PublishAsync(command);
    }

    /// <summary>
    ///     Update
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Description("update role")]
    [Authorize(ApplicationPermissions.Roles.Update)]
    [Consumes(MediaTypeNames.Application.Json)]
    [HttpPut]
    public async Task UpdateAsync([FromBody] UpdateRoleCommand command)
    {
        await CommandBus.PublishAsync(command);
    }

    /// <summary>
    ///     Delete
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Description("delete role")]
    [Authorize(ApplicationPermissions.Roles.Delete)]
    [HttpDelete("{id}")]
    public async Task DeleteAsync([FromRoute] Guid id)
    {
        await CommandBus.PublishAsync(new RemoveRoleCommand(id));
    }

    /// <summary>
    ///     Get detail
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Description("get role details")]
    [Authorize(ApplicationPermissions.Roles.Read)]
    [HttpGet("{id}")]
    public async Task<RoleDetailsDto> GetAsync([FromRoute] Guid id)
    {
        return await QueryProcessor.ProcessAsync(new GetRoleDetailsQuery(id));
    }

    /// <summary>
    ///     Get paged list
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    [Description("get role paged list")]
    [Authorize(ApplicationPermissions.Roles.Read)]
    [HttpGet("paged-list")]
    public async Task<PagedResultDto<RoleDto>> GetPagedListAsync([FromQuery] GetRolePagedListQuery query)
    {
        return await QueryProcessor.ProcessAsync(query);
    }
}
