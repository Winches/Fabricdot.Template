using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fabricdot.Infrastructure.Queries;
using Fabricdot.PermissionGranting;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.UserAggregate;

namespace ProjectName.WebApi.Application.Queries.Permissions
{
    internal class GetUserPermissionsQueryHandler : IQueryHandler<GetUserPermissionsQuery, ICollection<string>>
    {
        private readonly IPermissionGrantingManager _permissionGrantingManager;
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly UserManager<User> _userManager;

        public GetUserPermissionsQueryHandler(
            IPermissionGrantingManager permissionGrantingManager,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
            UserManager<User> userManager)
        {
            _permissionGrantingManager = permissionGrantingManager;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
            _userManager = userManager;
        }

        public async Task<ICollection<string>> Handle(
            GetUserPermissionsQuery request,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
                return Array.Empty<string>();

            var claimsPrincipal = await _userClaimsPrincipalFactory.CreateAsync(user);
            var grants = await _permissionGrantingManager.ListAsync(claimsPrincipal, cancellationToken);

            return grants.Select(v => v.Object).Distinct().ToArray();
        }
    }
}