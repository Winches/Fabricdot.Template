using AutoMapper;
using Fabricdot.Domain.Services;
using Fabricdot.Identity.Domain.Repositories;
using Fabricdot.Identity.Domain.Specifications;
using Fabricdot.Infrastructure.Queries;
using Fabricdot.WebApi.Models;
using ProjectName.Domain.Aggregates.RoleAggregate;
using ProjectName.Domain.Aggregates.UserAggregate;
using ProjectName.WebApi.Application.Queries.Roles;

namespace ProjectName.WebApi.Application.Queries.Users;

internal class GetUserPagedListQueryHandler : QueryHandler<GetUserPagedListQuery, PagedResultDto<UserDetailsDto>>
{
    private readonly IReadOnlyRepository<User> _userRepository;
    private readonly IRoleRepository<Role> _roleRepository;
    private readonly IMapper _mapper;

    public GetUserPagedListQueryHandler(
        IReadOnlyRepository<User> userRepository,
        IRoleRepository<Role> roleRepository,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public override async Task<PagedResultDto<UserDetailsDto>> ExecuteAsync(
        GetUserPagedListQuery query,
        CancellationToken cancellationToken)
    {
        var specification = new UserPagedListSpecification<User>(
            query.Index,
            query.Size,
            query.Filter,
            query.IsActive,
            query.IsLockedOut,
            includeDetails: true);
        var users = await _userRepository.ListAsync(specification, cancellationToken);
        var total = await _userRepository.CountAsync(specification, cancellationToken);
        var list = _mapper.Map<ICollection<UserDetailsDto>>(users);
        if (list.Count > 0)
        {
            var roleIds = users.Select(v => v.Roles.Select(o => o.RoleId))
                  .SelectMany(v => v)
                  .Distinct()
                  .ToArray();
            var roles = await _roleRepository.ListAsync(roleIds, cancellationToken: cancellationToken);
            var roleDtos = _mapper.Map<ICollection<RoleDto>>(roles);

            list.ForEach(v =>
            {
                var user = users.Single(o => o.Id == v.Id);
                var userRoleIds = user.Roles.Select(o => o.RoleId);
                v.Roles = roleDtos.Where(o => userRoleIds.Contains(o.Id))
                                  .ToArray();
            });
        }

        return new PagedResultDto<UserDetailsDto>(list, total);
    }
}
