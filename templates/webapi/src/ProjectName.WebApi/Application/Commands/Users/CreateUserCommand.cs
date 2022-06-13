using System;
using System.ComponentModel.DataAnnotations;
using Fabricdot.Identity.Domain.Constants;
using Fabricdot.Infrastructure.Commands;
using ProjectName.Domain.Shared.Constants;

namespace ProjectName.WebApi.Application.Commands.Users
{
    public class CreateUserCommand : CommandBase<Guid>
    {
        [Required]
        [MaxLength(IdentityUserConstant.UserNameLength)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(UserConstants.PasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MaxLength(IdentityUserConstant.GivenNameLength)]
        public string GivenName { get; set; }

        [MaxLength(IdentityUserConstant.SurnameLength)]
        public string Surname { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(1)]
        public Guid[] OrganizationIds { get; set; } = Array.Empty<Guid>();
    }
}