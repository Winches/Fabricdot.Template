using AutoMapper;
using ProjectName.Domain.Aggregates.RoleAggregate;

namespace ProjectName.WebApi.Application.Queries.Roles;

[AutoMap(typeof(Role))]
public class RoleDto
{
    public Guid Id { get; set; }

    public Guid? TenantId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsStatic { get; set; }

    public bool IsDefault { get; set; }
}
