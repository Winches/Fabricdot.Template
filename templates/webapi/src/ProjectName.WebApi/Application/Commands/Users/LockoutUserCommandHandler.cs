using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;
using ProjectName.Domain.Shared.Constants;

namespace ProjectName.WebApi.Application.Commands.Users
{
    internal class LockoutUserCommandHandler : ICommandHandler<LockoutUserCommand>
    {
        private readonly UserManager<User> _userManager;

        public LockoutUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(
            LockoutUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            Guard.Against.Null(user, nameof(user));

            var res = await _userManager.SetLockoutEnabledAsync(user, true);
            res.EnsureSuccess();
            var lockoutEnd = request.LockoutEnd ?? DateTimeOffset.UtcNow.AddDays(UserConstants.DefaultLockDays);
            res = await _userManager.SetLockoutEndDateAsync(user, lockoutEnd);
            res.EnsureSuccess();

            return Unit.Value;
        }
    }
}