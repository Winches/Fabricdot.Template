using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.RoleAggregate;

namespace ProjectName.WebApi.Application.Commands.Roles
{
    internal class RemoveRoleCommandHandler : ICommandHandler<RemoveRoleCommand>
    {
        private readonly RoleManager<Role> _roleManager;

        public RemoveRoleCommandHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(
            RemoveRoleCommand request,
            CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
            Guard.Against.Null(role, nameof(role));
            await _roleManager.DeleteAsync(role);
            return Unit.Value;
        }
    }
}