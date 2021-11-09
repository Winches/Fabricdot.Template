using Fabricdot.Infrastructure.DependencyInjection;
using FabricdotApp.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FabricdotApp.Infrastructure
{
    public class FabricdotAppInfrastructureModule : IModule
    {
        private readonly IConfiguration _configuration;

        public FabricdotAppInfrastructureModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(IServiceCollection services)
        {
            services.AddTransient<DbMigrator>();
            services.AddTransient<DataBuilder>();
        }
    }
}