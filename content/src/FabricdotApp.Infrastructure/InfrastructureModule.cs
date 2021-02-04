using Fabricdot.Infrastructure.Core.DependencyInjection;
using FabricdotApp.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace FabricdotApp.Infrastructure
{
    public class InfrastructureModule : IModule
    {
        public void Configure(IServiceCollection services)
        {
            services.AddTransient<DbMigrator>();
            services.AddTransient<DataBuilder>();
        }
    }
}