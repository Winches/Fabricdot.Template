using Fabricdot.Identity.Domain.Entities.UserAggregate;

namespace ProjectName.Domain.Aggregates.UserAggregate;

public class User : IdentityUser
{
    public User(
        Guid userId,
        string userName,
        string givenName,
        string? surname = null,
        string? email = null) : base(userId, userName)
    {
        GivenName = givenName;
        Surname = surname;
        Email = email?.Trim();
        EmailConfirmed = false;
        Enable();
    }

    private User()
    {
    }
}
