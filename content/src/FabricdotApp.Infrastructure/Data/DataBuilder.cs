using System.Threading.Tasks;
using Fabricdot.Core.DependencyInjection;

namespace FabricdotApp.Infrastructure.Data
{
    internal class DataBuilder : ITransientDependency
    {
        public Task SeedAsync()
        {
            //seed data
            return Task.CompletedTask;
        }
    }
}