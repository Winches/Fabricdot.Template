using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;

namespace ProjectName.WebApi.Application.Commands.Users
{
    internal class UpdateUserRolesCommandHandler : ICommandHandler<UpdateUserRolesCommand>
    {
        private readonly UserManager<User> _userManager;

        public UpdateUserRolesCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(
            UpdateUserRolesCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            Guard.Against.Null(user, nameof(user));

            user.ClearRoles();
            var res = await _userManager.AddToRolesAsync(user, request.RoleNames);
            res.EnsureSuccess();

            return Unit.Value;
        }
    }
}