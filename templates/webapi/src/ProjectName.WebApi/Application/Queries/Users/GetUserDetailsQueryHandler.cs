using AutoMapper;
using Fabricdot.Identity.Domain.Repositories;
using Fabricdot.Infrastructure.Queries;
using ProjectName.Domain.Aggregates.RoleAggregate;
using ProjectName.Domain.Aggregates.UserAggregate;
using ProjectName.WebApi.Application.Queries.Roles;

namespace ProjectName.WebApi.Application.Queries.Users;

internal class GetUserDetailsQueryHandler : QueryHandler<GetUserDetailsQuery, UserDetailsDto?>
{
    private readonly IUserRepository<User> _userRepository;
    private readonly IRoleRepository<Role> _roleRepository;
    private readonly IMapper _mapper;

    public GetUserDetailsQueryHandler(
        IUserRepository<User> userRepository,
        IRoleRepository<Role> roleRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public override async Task<UserDetailsDto?> ExecuteAsync(
        GetUserDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId, cancellationToken: cancellationToken);
        if (user == null)
            return null;
        var roleIds = user.Roles.Select(v => v.RoleId)
                                .ToArray();
        var roles = await _roleRepository.ListAsync(roleIds, cancellationToken: cancellationToken);
        var ret = _mapper.Map<UserDetailsDto>(user);
        ret.Roles = _mapper.Map<ICollection<RoleDto>>(roles);

        return ret;
    }
}
