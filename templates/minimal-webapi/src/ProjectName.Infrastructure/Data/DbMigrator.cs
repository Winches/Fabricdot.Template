using Fabricdot.Core.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ProjectName.Infrastructure.Data;

public sealed class DbMigrator(IServiceProvider serviceProvider) : ITransientDependency
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;

    public async Task MigrateAsync()
    {
        using var scope = _serviceProvider.CreateScope();
        var serviceProvider = scope.ServiceProvider;
        await serviceProvider.GetRequiredService<AppDbContext>()
            .Database
            .MigrateAsync();
        await serviceProvider.GetRequiredService<DataBuilder>().SeedAsync();
    }
}
