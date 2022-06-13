using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Fabricdot.WebApi.Endpoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Infrastructure.Security;
using ProjectName.WebApi.Application.Commands.Users;
using ProjectName.WebApi.Application.Queries.Roles;
using ProjectName.WebApi.Application.Queries.Users;
using ProjectName.WebApi.Authorization;

namespace ProjectName.WebApi.Endpoints
{
    [DefaultAuthorize]
    [Route("api/user")]
    public class UserRoleController : EndPointBase
    {
        /// <summary>
        ///     Create
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [Description("add role to user")]
        [Authorize(ApplicationPermissions.Users.ManageRole)]
        [HttpPost("{id}/role/{role}")]
        public async Task CreateAsync(
            [FromRoute] Guid id,
            [FromRoute] string role)
        {
            await Sender.Send(new AddUserRolesCommand(id, new[] { role }));
        }

        /// <summary>
        ///     Update
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Description("update roles of user")]
        [Authorize(ApplicationPermissions.Users.ManageRole)]
        [HttpPut("roles")]
        public async Task UpdateUserRolesAsync([FromBody] UpdateUserRolesCommand command)
        {
            await Sender.Send(command);
        }

        /// <summary>
        ///     Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [Description("delete role to user")]
        [Authorize(ApplicationPermissions.Users.ManageRole)]
        [HttpDelete("{id}/role/{role}")]
        public async Task DeleteAsync(
            [FromRoute] Guid id,
            [FromRoute] string role)
        {
            await Sender.Send(new RemoveUserRolesCommand(id, new[] { role }));
        }

        /// <summary>
        ///     List roles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Description("list roles")]
        [Authorize(ApplicationPermissions.Users.ManageRole)]
        [HttpGet("{id}/roles")]
        public async Task<ICollection<RoleDto>> GetRolesAsync([FromRoute] Guid id)
        {
            return await Sender.Send(new GetUserRolesQuery(id));
        }

        /// <summary>
        ///     List roles of current user
        /// </summary>
        /// <returns></returns>
        [Description("list roles of user")]
        [HttpGet("current/roles")]
        public async Task<ICollection<RoleDto>> GetCurrentRolesAsync()
        {
            var userId = Guid.Parse(CurrentUser.Id!);
            return await Sender.Send(new GetUserRolesQuery(userId));
        }
    }
}