using Fabricdot.Core.Modularity;
using Fabricdot.Infrastructure;
using Fabricdot.Infrastructure.Data;
using Fabricdot.Infrastructure.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProjectName.Domain;
using ProjectName.Infrastructure.Data;
using ProjectName.Infrastructure.Data.TypeHandlers;

namespace ProjectName.Infrastructure;

[Requires(typeof(AppNameDomainModule))]
[Requires(typeof(FabricdotEntityFrameworkCoreModule))]
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
    }
}
