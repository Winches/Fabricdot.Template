using System.ComponentModel.DataAnnotations;
using Fabricdot.Infrastructure.Commands;
using ProjectName.Infrastructure.Security.Authentication;

namespace ProjectName.WebApi.Application.Commands.Authentication
{
    public class AuthenticateCommand : Command<JwtTokenValue>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}