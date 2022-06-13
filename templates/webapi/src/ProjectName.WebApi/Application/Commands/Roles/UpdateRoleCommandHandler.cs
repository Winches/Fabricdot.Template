using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Fabricdot.Identity.Domain.Repositories;
using Fabricdot.Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.RoleAggregate;

namespace ProjectName.WebApi.Application.Commands.Roles
{
    internal class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand>
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

        public async Task<Unit> Handle(
            UpdateRoleCommand request,
            CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetDetailsByIdAsync(request.RoleId);
            Guard.Against.Null(role, nameof(role));

            await _roleManager.SetRoleNameAsync(role, request.Name);
            role.Description = request.Description;
            role.IsDefault = role.IsDefault;
            await _roleManager.UpdateAsync(role);

            return Unit.Value;
        }
    }
}