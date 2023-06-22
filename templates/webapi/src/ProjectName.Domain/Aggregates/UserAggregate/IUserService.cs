using Fabricdot.Domain.Services;

namespace ProjectName.Domain.Aggregates.UserAggregate;

/// <summary>
///     User domain service
/// </summary>
public interface IUserService : IDomainService
{
    Task EnsurePhoneNumberIsUniqueAsync(
        User user,
        CancellationToken cancellationToken = default);
}
