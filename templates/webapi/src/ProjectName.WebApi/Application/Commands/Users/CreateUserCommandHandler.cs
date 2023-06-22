using Fabricdot.Core.UniqueIdentifier;
using Fabricdot.Identity.Domain.Repositories;
using Fabricdot.Infrastructure.Commands;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.RoleAggregate;
using ProjectName.Domain.Aggregates.UserAggregate;
using ProjectName.Domain.Specifications;

namespace ProjectName.WebApi.Application.Commands.Users;

internal class CreateUserCommandHandler : CommandHandler<CreateUserCommand, Guid>
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;
    private readonly IRoleRepository<Role> _roleRepository;

    public CreateUserCommandHandler(
        IGuidGenerator guidGenerator,
        UserManager<User> userManager,
        IUserService userService,
        IRoleRepository<Role> roleRepository)
    {
        _guidGenerator = guidGenerator;
        _userManager = userManager;
        _userService = userService;
        _roleRepository = roleRepository;
    }

    public override async Task<Guid> ExecuteAsync(
        CreateUserCommand command,
        CancellationToken cancellationToken)
    {
        var user = new User(
            _guidGenerator.Create(),
            command.UserName,
            command.GivenName.Trim(),
            command.Surname?.Trim(),
            command.Email);
        user.ChangePhoneNumber(command.PhoneNumber);
        await _userService.EnsurePhoneNumberIsUniqueAsync(
            user,
            cancellationToken);

        // add default roles
        var roles = await _roleRepository.ListAsync(new RoleFilterSpec(true), cancellationToken);
        roles.ForEach(v => user.AddRole(v.Id));
        var res = await _userManager.CreateAsync(user, command.Password);
        res.EnsureSuccess();
        return user.Id;
    }
}
