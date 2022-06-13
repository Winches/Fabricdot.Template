using System;
using System.ComponentModel.DataAnnotations;
using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Users
{
    public class LockoutUserCommand : CommandBase
    {
        [Required]
        public Guid UserId { get; set; }

        public DateTimeOffset? LockoutEnd { get; set; }
    }
}