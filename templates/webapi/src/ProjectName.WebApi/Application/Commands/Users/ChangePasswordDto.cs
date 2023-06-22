using System.ComponentModel.DataAnnotations;
using ProjectName.Domain.Shared.Constants;

namespace ProjectName.WebApi.Application.Commands.Users;

public class ChangePasswordDto
{
    [Required]
    [MaxLength(UserConstants.PasswordLength)]
    [DataType(DataType.Password)]
    public string CurrentPassword { get; set; } = null!;

    [Required]
    [MaxLength(UserConstants.PasswordLength)]
    [DataType(DataType.Password)]
    public string NewPassword { get; set; } = null!;

    [Required]
    [MaxLength(UserConstants.PasswordLength)]
    [Compare(nameof(NewPassword))]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = null!;
}
