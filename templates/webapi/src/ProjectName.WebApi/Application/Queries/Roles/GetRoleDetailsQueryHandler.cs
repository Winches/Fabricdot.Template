using AutoMapper;
using Fabricdot.Identity.Domain.Repositories;
using Fabricdot.Infrastructure.Queries;
using ProjectName.Domain.Aggregates.RoleAggregate;

namespace ProjectName.WebApi.Application.Queries.Roles;

internal class GetRoleDetailsQueryHandler : QueryHandler<GetRoleDetailsQuery, RoleDetailsDto>
{
    private readonly IRoleRepository<Role> _roleRepository;
    private readonly IMapper _mapper;

    public GetRoleDetailsQueryHandler(
        IRoleRepository<Role> roleRepository,
        IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public override async Task<RoleDetailsDto> ExecuteAsync(
        GetRoleDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var role = await _roleRepository.GetByIdAsync(query.RoleId, cancellationToken: cancellationToken);
        return _mapper.Map<RoleDetailsDto>(role);
    }
}
