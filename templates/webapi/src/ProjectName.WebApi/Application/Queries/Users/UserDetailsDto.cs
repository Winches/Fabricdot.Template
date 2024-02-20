using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Fabricdot.Domain.Auditing;
using ProjectName.Domain.Aggregates.UserAggregate;
using ProjectName.WebApi.Application.Queries.Roles;

namespace ProjectName.WebApi.Application.Queries.Users;

[AutoMap(typeof(User))]
public class UserDetailsDto : IAuditEntity
{
    public Guid Id { get; set; }

    public string UserName { get; set; } = null!;

    public string GivenName { get; set; } = null!;

    public string? Surname { get; set; }

    public string? Email { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PhoneNumber { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool IsActive { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public bool LockoutEnabled { get; set; }

    public bool HasPassword { get; set; }

    public bool IsLockedOut { get; set; }

    /// <inheritdoc />
    public DateTime CreationTime { get; set; }

    /// <inheritdoc />
    public DateTime? LastModificationTime { get; set; }

    /// <inheritdoc />
    public string CreatorId { get; set; } = null!;

    /// <inheritdoc />
    public string? LastModifierId { get; set; }

    [Ignore]
    public ICollection<RoleDto> Roles { get; set; } = [];
}
