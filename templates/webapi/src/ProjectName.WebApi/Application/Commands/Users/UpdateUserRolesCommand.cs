using System.ComponentModel.DataAnnotations;
using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Users;

public class UpdateUserRolesCommand : Command
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public string[] RoleNames { get; set; } = [];
}
