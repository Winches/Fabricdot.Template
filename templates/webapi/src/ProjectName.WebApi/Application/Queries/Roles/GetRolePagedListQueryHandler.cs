using AutoMapper;
using Fabricdot.Domain.Services;
using Fabricdot.Identity.Domain.Specifications;
using Fabricdot.Infrastructure.Queries;
using Fabricdot.WebApi.Models;
using ProjectName.Domain.Aggregates.RoleAggregate;

namespace ProjectName.WebApi.Application.Queries.Roles;

internal class GetRolePagedListQueryHandler : QueryHandler<GetRolePagedListQuery, PagedResultDto<RoleDto>>
{
    private readonly IReadOnlyRepository<Role> _roleRepository;
    private readonly IMapper _mapper;

    public GetRolePagedListQueryHandler(
        IReadOnlyRepository<Role> roleRepository,
        IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public override async Task<PagedResultDto<RoleDto>> ExecuteAsync(
        GetRolePagedListQuery query,
        CancellationToken cancellationToken)
    {
        var specification = new RolePagedListSpecification<Role>(
            query.Index,
            query.Size,
            query.Filter);
        var list = await _roleRepository.ListAsync(specification, cancellationToken);
        var total = await _roleRepository.CountAsync(specification, cancellationToken);

        return new PagedResultDto<RoleDto>(_mapper.Map<ICollection<RoleDto>>(list), total);
    }
}
