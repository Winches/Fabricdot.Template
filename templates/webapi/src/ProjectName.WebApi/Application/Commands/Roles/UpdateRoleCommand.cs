using System.ComponentModel.DataAnnotations;
using Fabricdot.Identity.Domain.Constants;
using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Roles;

public class UpdateRoleCommand : Command
{
    [Required]
    public Guid RoleId { get; set; }

    [Required]
    [MaxLength(IdentityRoleConstant.NameLength)]
    public string Name { get; set; } = null!;

    [MaxLength(IdentityRoleConstant.DescriptionLength)]
    public string? Description { get; set; }

    public bool IsDefault { get; set; }
}
