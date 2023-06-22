namespace ProjectName.WebApi.Application.Queries.Users;

public class GetUserDetailsQuery : UserQueryBase<UserDetailsDto?>
{
    public GetUserDetailsQuery(Guid userId) : base(userId)
    {
    }
}
