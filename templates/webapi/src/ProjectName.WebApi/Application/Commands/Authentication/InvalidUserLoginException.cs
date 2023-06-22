using System.Diagnostics.CodeAnalysis;
using Fabricdot.Infrastructure.Commands;

namespace ProjectName.WebApi.Application.Commands.Authentication;

[SuppressMessage("Roslynator", "RCS1194:Implement exception constructors.", Justification = "<Pending>")]
public class InvalidUserLoginException : CommandException
{
    public const int ErrorCode = 901;

    public InvalidUserLoginException(string message = "User login is incorrect.") : base(message, ErrorCode)
    {
    }
}
