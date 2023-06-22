using Ardalis.GuardClauses;
using Fabricdot.Infrastructure.Security;

namespace ProjectName.Infrastructure.Security;

public static class CurrentUserExensions
{
    public static Guid? GetId(this ICurrentUser currentUser)
    {
        Guard.Against.Null(currentUser, nameof(currentUser));
        return currentUser.Id is null ? null : Guid.Parse(currentUser.Id);
    }
}
