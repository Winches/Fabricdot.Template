﻿using System.Threading;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;

namespace ProjectName.WebApi.Application.Commands.Users
{
    internal class RemoveUserCommandHandler : CommandHandler<RemoveUserCommand>
    {
        private readonly UserManager<User> _userManager;

        public RemoveUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<Unit> ExecuteAsync(
            RemoveUserCommand command,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(command.UserId.ToString());
            Guard.Against.Null(user, nameof(user));

            var res = await _userManager.DeleteAsync(user);
            res.EnsureSuccess();

            return Unit.Value;
        }
    }
}