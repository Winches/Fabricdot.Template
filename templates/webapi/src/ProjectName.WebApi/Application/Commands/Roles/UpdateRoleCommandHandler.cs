using Ardalis.GuardClauses;
using Fabricdot.Identity.Domain.Repositories;
using Fabricdot.Infrastructure.Commands;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.RoleAggregate;

namespace ProjectName.WebApi.Application.Commands.Roles;

internal class UpdateRoleCommandHandler : CommandHandler<UpdateRoleCommand>
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IRoleRepository<Role> _roleRepository;

    public UpdateRoleCommandHandler(
        RoleManager<Role> roleManager,
        IRoleRepository<Role> roleRepository)
    {
        _roleManager = roleManager;
        _roleRepository = roleRepository;
    }

    public override async Task ExecuteAsync(
        UpdateRoleCommand command,
        CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(command.RoleId, cancellationToken: cancellationToken);
        Guard.Against.Null(role, nameof(role));

        await _roleManager.SetRoleNameAsync(role, command.Name);
        role.Description = command.Description;
        role.IsDefault = role.IsDefault;
        await _roleManager.UpdateAsync(role);
    }
}
