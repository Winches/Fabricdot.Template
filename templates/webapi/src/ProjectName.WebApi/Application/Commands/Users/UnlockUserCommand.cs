using System.ComponentModel.DataAnnotations;
using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Users;

public class UnlockUserCommand : Command
{
    [Required]
    public Guid UserId { get; set; }
}
