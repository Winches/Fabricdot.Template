using System.Reflection;
using Fabricdot.Identity.Infrastructure.Data;
using Fabricdot.PermissionGranting.Domain;
using Fabricdot.PermissionGranting.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ProjectName.Domain.Aggregates.RoleAggregate;
using ProjectName.Domain.Aggregates.UserAggregate;

namespace ProjectName.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User, Role>, IPermissionGrantingDbContext
    {
        public DbSet<GrantedPermission> GrantedPermissions { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            builder.ConfigurePermissionGranting();
        }
    }
}