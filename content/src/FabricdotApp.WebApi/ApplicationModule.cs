using System;
using Fabricdot.Domain.Core.SharedKernel;
using Fabricdot.Infrastructure.Core.Data;
using Fabricdot.Infrastructure.Core.DependencyInjection;
using Fabricdot.WebApi.Core.Configuration;
using FabricdotApp.Infrastructure.Data;
using FabricdotApp.Infrastructure.Data.TypeHandlers;
using FabricdotApp.WebApi.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FabricdotApp.WebApi
{
    public class ApplicationModule : IModule
    {
        private readonly IConfiguration _configuration;
        public static readonly ILoggerFactory DbLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });


        public ApplicationModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <inheritdoc />
        public void Configure(IServiceCollection services)
        {
            #region endpoint

            services.AddControllers(opts => opts.AddActionFilters())
                .ConfigureApiBehaviorOptions(opts =>
                {
                    opts.SuppressModelStateInvalidFilter = true;
                });

            #endregion endpoint

            #region database

            services.AddDbContext<AppDbContext>(opts =>
            {
                //todo:use database
#if DEBUG
                opts.UseLoggerFactory(DbLoggerFactory)
                    .EnableSensitiveDataLogging();
#endif
            });

            SqlMapperTypeHandlerConfiguration.AddTypeHandlers();
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>(provider =>
                new SqlConnectionFactory(_configuration.GetConnectionString("Default")));

            #endregion database

            #region api-doc

            //swagger
            services.AddSwagger();

            #endregion

            SystemClock.Configure(DateTimeKind.Utc);

            services.AddScoped<IEntityChangeTracker, EntityChangeTracker>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            //add project services here.
        }
    }
}