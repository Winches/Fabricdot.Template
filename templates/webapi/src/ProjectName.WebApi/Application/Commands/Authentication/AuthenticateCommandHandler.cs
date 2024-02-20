using Fabricdot.Domain.SharedKernel;
using Fabricdot.Infrastructure.Commands;
using Fabricdot.Infrastructure.Data.Filters;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;
using ProjectName.Infrastructure.Security.Authentication;

namespace ProjectName.WebApi.Application.Commands.Authentication;

internal class AuthenticateCommandHandler : CommandHandler<AuthenticateCommand, JwtTokenValue>
{
    private readonly IDataFilter _dataFilter;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtSecurityTokenService _jwtSecurityTokenService;

    public AuthenticateCommandHandler(
        IDataFilter dataFilter,
        SignInManager<User> signInManager,
        IJwtSecurityTokenService jwtSecurityTokenService)
    {
        _dataFilter = dataFilter;
        _signInManager = signInManager;
        _jwtSecurityTokenService = jwtSecurityTokenService;
    }

    public override async Task<JwtTokenValue> ExecuteAsync(
        AuthenticateCommand command,
        CancellationToken cancellationToken)
    {
        var user = await FindUserAsync(command);
        var signInResult = await _signInManager.PasswordSignInAsync(user, command.Password, false, true);
        if (!signInResult.Succeeded)
        {
            if (signInResult.IsNotAllowed)// confirm email or phone number
                throw new UserNotAllowedException();

            if (signInResult.IsLockedOut)
                throw new UserLockedOutException();

            throw new InvalidUserPasswordException();
        }

        if (!user.IsActive)
            throw new CommandException("User is inactive.");

        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
        return await _jwtSecurityTokenService.CreateTokenAsync(claimsPrincipal);

        async Task<User> FindUserAsync(AuthenticateCommand request)
        {
            using var scope = _dataFilter.Disable<IMultiTenant>();
            var user = await _signInManager.UserManager.FindByNameAsync(request.UserName);
            return user ?? throw new InvalidUserLoginException();
        }
    }
}
