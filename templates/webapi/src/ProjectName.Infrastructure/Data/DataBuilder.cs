using Fabricdot.Authorization;
using Fabricdot.Authorization.Permissions;
using Fabricdot.Core.DependencyInjection;
using Fabricdot.Core.UniqueIdentifier;
using Fabricdot.Infrastructure.Uow.Abstractions;
using Fabricdot.PermissionGranting;
using Microsoft.AspNetCore.Identity;
using ProjectName.Domain.Aggregates.RoleAggregate;
using ProjectName.Domain.Aggregates.UserAggregate;
using ProjectName.Infrastructure.Security;
using P = ProjectName.Infrastructure.Security.ApplicationPermissions;

namespace ProjectName.Infrastructure.Data;

internal class DataBuilder(
    IUnitOfWorkManager unitOfWorkManager,
    IGuidGenerator guidGnerator,
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    IPermissionManager permissionManager,
    IPermissionGrantingManager permissionGrantingManager) : ITransientDependency
{
    protected const string AdminUserName = "admin";
    protected const string AdminPassword = "Administrator@123";
    private readonly IUnitOfWorkManager _unitOfWorkManager = unitOfWorkManager;
    private readonly IGuidGenerator _guidGnerator = guidGnerator;
    private readonly UserManager<User> _userManager = userManager;
    private readonly RoleManager<Role> _roleManager = roleManager;
    private readonly IPermissionManager _permissionManager = permissionManager;
    private readonly IPermissionGrantingManager _permissionGrantingManager = permissionGrantingManager;

    public virtual async Task SeedAsync()
    {
        using var uow = _unitOfWorkManager.Begin();
        // Seed data
        await SeedPermissionAsync();
        await SeedAdministratorAsync();
        await uow.CommitChangesAsync();
    }

    protected virtual async Task SeedPermissionAsync()
    {
        var group = new PermissionGroup("Permission", "Function Permissions");
        // Seed permission
        group.AddPermission(P.Users.Name, "User")
             .Add(P.Users.Create, "Create")
             .Add(P.Users.Read, "Read")
             .Add(P.Users.Update, "Update")
             .Add(P.Users.Delete, "Delete")
             .Add(P.Users.ManageRole, "Manage Role")
             .Add(P.Users.ManagePermission, "Manage Permission")
             .Add(P.Users.View, "Manage");

        group.AddPermission(P.Roles.Name, "Role")
             .Add(P.Roles.Create, "Create")
             .Add(P.Roles.Read, "Read")
             .Add(P.Roles.Update, "Update")
             .Add(P.Roles.Delete, "Delete")
             .Add(P.Roles.ManagePermission, "Manage Permission")
             .Add(P.Roles.View, "Manage");

        await _permissionManager.AddGroupAsync(group);
    }

    protected virtual async Task SeedAdministratorAsync()
    {
        var administrator = await _userManager.FindByNameAsync(AdminUserName);
        var adminRole = await _roleManager.FindByNameAsync(ApplicationRoles.Administrator);

        if (adminRole == null)
        {
            adminRole = new Role(
                _guidGnerator.Create(),
                ApplicationRoles.Administrator,
                true)
            {
                Description = "Administrator"
            };
            await _roleManager.CreateAsync(adminRole);
        }

        if (administrator == null)
        {
            administrator = new User(
                _guidGnerator.Create(),
                AdminUserName,
                "Administrator");
            administrator.AddRole(adminRole.Id);
            await _userManager.CreateAsync(administrator, AdminPassword);
        }

        await _permissionGrantingManager.GrantAsync(
            GrantSubject.User(administrator.Id.ToString()),
            StandardPermissions.Superuser);
    }
}
