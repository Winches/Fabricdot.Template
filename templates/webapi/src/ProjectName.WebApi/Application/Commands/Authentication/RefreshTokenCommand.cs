using System.ComponentModel.DataAnnotations;
using Fabricdot.Infrastructure.Commands;
using ProjectName.Infrastructure.Security.Authentication;

namespace ProjectName.WebApi.Application.Commands.Authentication
{
    public class RefreshTokenCommand : Command<JwtTokenValue>
    {
        [Required]
        public string AccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}