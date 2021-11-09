using System;
using Fabricdot.Domain.SharedKernel;
using Fabricdot.Infrastructure.Data;
using Fabricdot.Infrastructure.DependencyInjection;
using Fabricdot.WebApi.Configuration;
using FabricdotApp.Infrastructure.Data;
using FabricdotApp.Infrastructure.Data.TypeHandlers;
using FabricdotApp.WebApi.Configuration;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FabricdotApp.WebApi
{
    public class FabricdotAppApplicationModule : IModule
    {
        private static readonly ILoggerFactory _dbLoggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
        private readonly IConfiguration _configuration;

        public FabricdotAppApplicationModule(IConfiguration configuration)
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

            var connectionString = _configuration.GetConnectionString("Default");
            services.AddEfDbContext<AppDbContext>(opts =>
            {
                //todo:use database

                opts.UseLoggerFactory(_dbLoggerFactory);
                //opts.EnableSensitiveDataLogging();
            });

            SqlMapperTypeHandlerConfiguration.AddTypeHandlers();
            services.AddScoped<ISqlConnectionFactory, DefaultSqlConnectionFactory>(_ => new DefaultSqlConnectionFactory(connectionString));

            #endregion database

            #region api-doc

            //swagger
            services.AddSwagger();

            #endregion api-doc

            SystemClock.Configure(DateTimeKind.Utc);
            services.AddSingleton<IContentTypeProvider, FileExtensionContentTypeProvider>();
            //add project services here.
        }
    }
}