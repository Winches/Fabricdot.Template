using System.ComponentModel.DataAnnotations;
using Fabricdot.Identity.Domain.Constants;
using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Users;

public class UpdateUserCommand : Command
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    [MaxLength(IdentityUserConstant.GivenNameLength)]
    public string GivenName { get; set; } = null!;

    [MaxLength(IdentityUserConstant.SurnameLength)]
    public string? Surname { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }

    [Required]
    [MinLength(1)]
    public Guid[] OrganizationIds { get; set; } = [];
}
