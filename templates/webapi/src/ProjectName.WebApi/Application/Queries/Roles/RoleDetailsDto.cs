using ProjectName.WebApi.Application.Models.ReadModels;

namespace ProjectName.WebApi.Application.Queries.Roles;

public class RoleDetailsDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsStatic { get; set; }

    public bool IsDefault { get; set; }

    public ICollection<ClaimDto> Claims { get; set; } = new List<ClaimDto>();
}
