using Fabricdot.Authorization.Permissions;

namespace ProjectName.Infrastructure.Security;

/// <summary>
///     Application permissions
/// </summary>
public static class ApplicationPermissions
{
    public static class Users
    {
        public const string Name = "User";
        public const string Read = Name + Separator + StandardPermissions.Operations.Read;
        public const string Create = Name + Separator + StandardPermissions.Operations.Create;
        public const string Delete = Name + Separator + StandardPermissions.Operations.Delete;
        public const string Update = Name + Separator + StandardPermissions.Operations.Update;
        public const string ManageRole = Name + Separator + nameof(ManageRole);
        public const string ManagePermission = Name + Separator + nameof(ManagePermission);
        public const string View = Name + Separator + nameof(View);
    }

    public static class Roles
    {
        public const string Name = "Role";
        public const string Read = Name + Separator + StandardPermissions.Operations.Read;
        public const string Create = Name + Separator + StandardPermissions.Operations.Create;
        public const string Delete = Name + Separator + StandardPermissions.Operations.Delete;
        public const string Update = Name + Separator + StandardPermissions.Operations.Update;
        public const string ManagePermission = Name + Separator + nameof(ManagePermission);
        public const string View = Name + Separator + nameof(View);
    }

    internal const string Separator = ".";
}
