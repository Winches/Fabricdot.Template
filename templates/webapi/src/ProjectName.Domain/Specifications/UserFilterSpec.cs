using Ardalis.Specification;
using ProjectName.Domain.Aggregates.UserAggregate;

namespace ProjectName.Domain.Specifications;

public sealed class UserFilterSpec : Specification<User>
{
    public UserFilterSpec(
        Guid userId,
        string phoneNumber)
    {
        Query.Where(v => v.Id != userId && v.PhoneNumber == phoneNumber);
    }
}
