using Fabricdot.Infrastructure.Core.DependencyInjection;
using Fabricdot.Infrastructure.Core.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Fabricdot.Infrastructure
{
    public class DomainModule : IModule
    {
        public void Configure(IServiceCollection services)
        {
            services.AddRepositories(GetType().Assembly);
            //todo:use domain assembly
            //services.AddDomainServices(typeof(Type).Assembly);
        }
    }
}