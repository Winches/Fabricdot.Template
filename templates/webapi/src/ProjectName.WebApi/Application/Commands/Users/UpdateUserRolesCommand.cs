using System;
using System.ComponentModel.DataAnnotations;
using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Users
{
    public class UpdateUserRolesCommand : CommandBase
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string[] RoleNames { get; set; } = Array.Empty<string>();
    }
}