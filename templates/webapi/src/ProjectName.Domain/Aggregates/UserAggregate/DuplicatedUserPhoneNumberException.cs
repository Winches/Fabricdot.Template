using System.Diagnostics.CodeAnalysis;
using Fabricdot.Domain.SharedKernel;

namespace ProjectName.Domain.Aggregates.UserAggregate;

[SuppressMessage("Roslynator", "RCS1194:Implement exception constructors.", Justification = "<Pending>")]
public class DuplicatedUserPhoneNumberException(string message = "Phone number is already taken.") : DomainException(message, ErrorCode)
{
    public const int ErrorCode = 1101;
}
