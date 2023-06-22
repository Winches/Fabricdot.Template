using Fabricdot.Identity.Domain.Entities.RoleAggregate;

namespace ProjectName.Domain.Aggregates.RoleAggregate;

public class Role : IdentityRole
{
    public bool IsStatic { get; private set; }

    public bool IsDefault { get; set; }

    public Role(
        Guid roleId,
        string roleName,
        bool isStatic = false) : base(roleId, roleName)
    {
        IsStatic = isStatic;
    }

    private Role()
    {
    }
}
