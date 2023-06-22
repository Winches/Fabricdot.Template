using System.Diagnostics.CodeAnalysis;
using Fabricdot.Domain.SharedKernel;

namespace ProjectName.Domain.Aggregates.UserAggregate;

[SuppressMessage("Roslynator", "RCS1194:Implement exception constructors.", Justification = "<Pending>")]
public class DuplicatedUserPhoneNumberException : DomainException
{
    public const int ErrorCode = 1101;

    public DuplicatedUserPhoneNumberException(string message = "Phone number is already taken.") : base(message, ErrorCode)
    {
    }
}
