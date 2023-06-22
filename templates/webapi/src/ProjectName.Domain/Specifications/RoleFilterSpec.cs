using Ardalis.Specification;
using ProjectName.Domain.Aggregates.RoleAggregate;

namespace ProjectName.Domain.Specifications;

public sealed class RoleFilterSpec : Specification<Role>
{
    public RoleFilterSpec(bool isDefault)
    {
        Query.Where(v => v.IsDefault == isDefault);
    }
}
