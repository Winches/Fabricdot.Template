using System;
using System.Collections.Generic;
using System.Linq;
using Fabricdot.Common.Core.Enumerable;
using Fabricdot.Domain.Core.Services;
using Fabricdot.Infrastructure.Core.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Fabricdot.Infrastructure
{
    public class DomainModule : IModule
    {
        public void Configure(IServiceCollection services)
        {
            var repositoryType = typeof(IRepository<,>);
            var domainServiceType = typeof(IDomainService);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(v => v.ManifestModule.Name.EndsWith(".Domain.dll") || v.ManifestModule.Name.EndsWith(".Infrastructure.dll"))
                .ToList();

            ConfigureRepositories(services, repositoryType, assemblies.Single(v => v.ManifestModule.Name.EndsWith("Infrastructure.dll")));

            ConfigureServices(services, domainServiceType, assemblies.Single(v => v.ManifestModule.Name.EndsWith("Domain.dll")));
        }

        private static void ConfigureServices(IServiceCollection services, Type domainServiceType, System.Reflection.Assembly assembly)
        {
            IEnumerable<(Type Contract, Type Implementation)> domainServices = assembly.GetTypes()
                .Select(v => new
                {
                    Type = v,
                    Interfaces = v.GetInterfaces()
                })
                .Where(v => v.Type.IsClass && !v.Type.IsAbstract && v.Interfaces.Contains(domainServiceType))
                .Select(v => (v.Interfaces.Single(o => !o.IsGenericType && IsSatisfyConvention(o, v.Type)), v.Type));

            domainServices.ForEach(v => services.AddScoped(v.Contract, v.Implementation));
        }

        private static void ConfigureRepositories(IServiceCollection services, Type reposiotryType, System.Reflection.Assembly assembly)
        {
            IEnumerable<(Type Contract, Type Implementation)> reposiotries = assembly.GetTypes()
                .Select(v => new
                {
                    Type = v,
                    Interfaces = v.GetInterfaces()
                })
                .Where(v => v.Type.IsClass && !v.Type.IsAbstract && v.Interfaces.Any(o => o.IsGenericType && o.GetGenericTypeDefinition() == reposiotryType))
                .Select(v => (v.Interfaces.Single(o => !o.IsGenericType && IsSatisfyConvention(o, v.Type)), v.Type));

            reposiotries.ForEach(v => services.AddScoped(v.Contract, v.Implementation));
        }

        private static bool IsSatisfyConvention(Type destination, Type source)
        {
            var name = source.IsInterface ? source.Name.Trim('I') : source.Name;
            return destination.Name.EndsWith(name);
        }
    }
}