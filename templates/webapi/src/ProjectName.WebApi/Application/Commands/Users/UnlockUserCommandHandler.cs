using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;

namespace ProjectName.WebApi.Application.Commands.Users
{
    internal class UnlockUserCommandHandler : ICommandHandler<UnlockUserCommand>
    {
        private readonly UserManager<User> _userManager;

        public UnlockUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(
            UnlockUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            Guard.Against.Null(user, nameof(user));

            if (user.IsLockedOut)
            {
                var res = await _userManager.SetLockoutEnabledAsync(user, true);
                res.EnsureSuccess();
                res = await _userManager.SetLockoutEndDateAsync(user, null);
                res.EnsureSuccess();
            }
            return Unit.Value;
        }
    }
}