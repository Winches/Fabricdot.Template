using System;
using Fabricdot.Domain.Core.SharedKernel;
using Fabricdot.Infrastructure.Core.Data;
using Fabricdot.Infrastructure.Core.DependencyInjection;
using Fabricdot.Infrastructure.Data;
using Fabricdot.WebApi.Configuration;
using Fabricdot.WebApi.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Fabricdot.WebApi
{
    public class ApplicationModule:IModule
    {
        public static readonly ILoggerFactory DbLoggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

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