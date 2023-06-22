using System.ComponentModel;
using Fabricdot.WebApi.Endpoint;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Infrastructure.Security.Authentication;
using ProjectName.WebApi.Application.Commands.Authentication;

namespace ProjectName.WebApi.Endpoints;

/// <summary>
///     Authenticate
/// </summary>
public class AuthenticateController : EndPointBase
{
    /// <summary>
    ///     login
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [Description("authenticate")]
    [HttpPost]
    public async Task<JwtTokenValue> AuthenticateAsync([FromBody] AuthenticateCommand command)
    {
        return await CommandBus.PublishAsync(command);
    }

    /// <summary>
    ///     refresh token
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [Description("refresh token")]
    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<JwtTokenValue> RefreshTokenAsync([FromBody] RefreshTokenCommand command)
    {
        return await CommandBus.PublishAsync(command);
    }
}
