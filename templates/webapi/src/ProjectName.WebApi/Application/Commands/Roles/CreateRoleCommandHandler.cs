using Fabricdot.Core.UniqueIdentifier;
using Fabricdot.Infrastructure.Commands;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.RoleAggregate;

namespace ProjectName.WebApi.Application.Commands.Roles;

internal class CreateRoleCommandHandler : CommandHandler<CreateRoleCommand, Guid>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IGuidGenerator _guidGenerator;

    public CreateRoleCommandHandler(
        RoleManager<Role> roleManager,
        IGuidGenerator guidGenerator)
    {
        _roleManager = roleManager;
        _guidGenerator = guidGenerator;
    }

    public override async Task<Guid> ExecuteAsync(
        CreateRoleCommand command,
        CancellationToken cancellationToken)
    {
        var role = new Role(_guidGenerator.Create(), command.Name, false)
        {
            Description = command.Description,
            IsDefault = command.IsDefault
        };
        await _roleManager.CreateAsync(role);
        return role.Id;
    }
}
