using System;
using System.Threading;
using System.Threading.Tasks;
using Fabricdot.Core.UniqueIdentifier;
using Fabricdot.Infrastructure.Commands;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.RoleAggregate;

namespace ProjectName.WebApi.Application.Commands.Roles
{
    internal class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, Guid>
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

        public async Task<Guid> Handle(
            CreateRoleCommand request,
            CancellationToken cancellationToken)
        {
            var role = new Role(_guidGenerator.Create(), request.Name, false)
            {
                Description = request.Description,
                IsDefault = request.IsDefault
            };
            await _roleManager.CreateAsync(role);
            return role.Id;
        }
    }
}