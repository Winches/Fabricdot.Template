using Fabricdot.Core.Modularity;
using Fabricdot.Infrastructure;
using Fabricdot.Infrastructure.Data;
using Fabricdot.Infrastructure.EntityFrameworkCore;
using FabricdotApp.Domain;
using FabricdotApp.Infrastructure.Data;
using FabricdotApp.Infrastructure.Data.TypeHandlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FabricdotApp.Infrastructure
{
    [Requires(typeof(FabricdotAppDomainModule))]
    [Requires(typeof(FabricdotEntityFrameworkCoreModule))]
    [Requires(typeof(FabricdotInfrastructureModule))]
    [Exports]
    public class FabricdotAppInfrastructureModule : ModuleBase
    {
        private static readonly ILoggerFactory _dbLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

        public override void ConfigureServices(ConfigureServiceContext context)
        {
            var services = context.Services;

            #region database

            var connectionString = context.Configuration.GetConnectionString("Default");
            services.AddEfDbContext<AppDbContext>(opts =>
            {
                //TODO:use database provider.

                opts.UseLoggerFactory(_dbLoggerFactory);
                //opts.EnableSensitiveDataLogging();
            });

            SqlMapperTypeHandlerConfiguration.AddTypeHandlers();
            services.AddScoped<ISqlConnectionFactory, DefaultSqlConnectionFactory>(_ => new DefaultSqlConnectionFactory(connectionString));

            #endregion database
        }
    }
}