using AutoMapper;
using Fabricdot.Identity.Domain.Entities.RoleAggregate;
using Fabricdot.Identity.Domain.Entities.UserAggregate;

namespace ProjectName.WebApi.Application.Models.ReadModels;

[AutoMap(typeof(IdentityUserClaim))]
[AutoMap(typeof(IdentityRoleClaim))]
public class ClaimDto
{
    public string ClaimType { get; set; } = null!;

    public string ClaimValue { get; set; } = null!;
}
