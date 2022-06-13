using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;

namespace ProjectName.WebApi.Application.Commands.Users
{
    internal class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
    {
        private readonly UserManager<User> _userManager;

        public UpdateUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(
            UpdateUserCommand request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            Guard.Against.Null(user, nameof(user));

            user.GivenName = request.GivenName.Trim();
            user.Surname = request.Surname?.Trim();
            await _userManager.SetEmailAsync(user, request.Email);
            await _userManager.SetPhoneNumberAsync(user, request.PhoneNumber);
            var res = await _userManager.UpdateAsync(user);
            res.EnsureSuccess();

            return Unit.Value;
        }
    }
}