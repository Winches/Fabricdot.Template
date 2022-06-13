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
    internal class SetDefaultPasswordCommandHadnler : ICommandHandler<SetDefaultPasswordCommand>
    {
        private readonly UserManager<User> _userManager;

        public SetDefaultPasswordCommandHadnler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(
            SetDefaultPasswordCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            Guard.Against.Null(user, nameof(user));
            var res = await _userManager.RemovePasswordAsync(user);
            res.EnsureSuccess();
            res = await _userManager.AddPasswordAsync(user, UserConstants.DefaultPassword);
            res.EnsureSuccess();
            return Unit.Value;
        }
    }
}