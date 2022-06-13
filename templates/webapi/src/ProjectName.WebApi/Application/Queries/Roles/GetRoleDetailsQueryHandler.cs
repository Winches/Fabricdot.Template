using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Fabricdot.Identity.Domain.Repositories;
using Fabricdot.Infrastructure.Queries;
using ProjectName.Domain.Aggregates.RoleAggregate;

namespace ProjectName.WebApi.Application.Queries.Roles
{
    internal class GetRoleDetailsQueryHandler : IQueryHandler<GetRoleDetailsQuery, RoleDetailsDto>
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

        public async Task<RoleDetailsDto> Handle(
            GetRoleDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var role = await _roleRepository.GetDetailsByIdAsync(request.RoleId);
            return _mapper.Map<RoleDetailsDto>(role);
        }
    }
}