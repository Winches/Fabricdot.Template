using Fabricdot.Domain.Services;
using Fabricdot.Identity.Domain.Repositories;
using ProjectName.Domain.Specifications;

namespace ProjectName.Domain.Aggregates.UserAggregate;

internal class UserService(IUserRepository<User> userRepository) : IUserService
{
    private readonly IUserRepository<User> _userRepository = userRepository;

    public async Task EnsurePhoneNumberIsUniqueAsync(
        User user,
        CancellationToken cancellationToken = default)
    {
        if (user.PhoneNumber.IsNullOrEmpty())
            return;
        var specification = new UserFilterSpec(user.Id, user.PhoneNumber!);
        if (await _userRepository.AnyAsync(specification, cancellationToken))
            throw new DuplicatedUserPhoneNumberException();
    }
}
