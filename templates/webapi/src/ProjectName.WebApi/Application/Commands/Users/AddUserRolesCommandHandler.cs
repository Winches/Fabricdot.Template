using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;

namespace ProjectName.WebApi.Application.Commands.Users
{
    internal class AddUserRolesCommandHandler : ICommandHandler<AddUserRolesCommand>
    {
        private readonly UserManager<User> _userManager;

        public AddUserRolesCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(
            AddUserRolesCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            Guard.Against.Null(user, nameof(user));

            var res = await _userManager.AddToRolesAsync(user, request.RoleNames);
            res.EnsureSuccess();

            return Unit.Value;
        }
    }
}