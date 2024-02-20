using Fabricdot.Core.Modularity;
using Fabricdot.Identity;
using Fabricdot.Infrastructure;
using Fabricdot.Infrastructure.Data;
using Fabricdot.Infrastructure.EntityFrameworkCore;
using Fabricdot.PermissionGranting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProjectName.Domain;
using ProjectName.Domain.Aggregates.RoleAggregate;
using ProjectName.Domain.Aggregates.UserAggregate;
using ProjectName.Infrastructure.Data;
using ProjectName.Infrastructure.Data.TypeHandlers;

namespace ProjectName.Infrastructure;

[Requires(typeof(AppNameDomainModule))]
[Requires(typeof(FabricdotIdentityModule))]
[Requires(typeof(FabricdotEntityFrameworkCoreModule))]
[Requires(typeof(FabricdotPermissionGrantingModule))]
[Requires(typeof(FabricdotInfrastructureModule))]
[Exports]
public class AppNameInfrastructureModule : ModuleBase
{
    private static readonly ILoggerFactory s_dbLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

    public override void ConfigureServices(ConfigureServiceContext context)
    {
        var services = context.Services;

        #region database

        var connectionString = context.Configuration.GetConnectionString("Default")!;
        services.AddEfDbContext<AppDbContext>(opts =>
        {
            // TODO:use database provider.

            opts.UseLoggerFactory(s_dbLoggerFactory);
            //-:cnd:noEmit
#if DEBUG
            opts.EnableSensitiveDataLogging();
#endif
            //+:cnd:noEmit
        });

        SqlMapperTypeHandlerConfiguration.AddTypeHandlers();
        services.AddScoped<ISqlConnectionFactory, DefaultSqlConnectionFactory>(_ => new DefaultSqlConnectionFactory(connectionString));

        #endregion database

        #region identity

        services.AddIdentity<User, Role>()
                .AddRepositories<AppDbContext>()
                .AddDefaultClaimsPrincipalFactory()
                .AddDefaultTokenProviders();

        #endregion identity

        #region permission-granting

        services.AddPermissionGrantingStore<AppDbContext>();

        #endregion permission-granting
    }
}
