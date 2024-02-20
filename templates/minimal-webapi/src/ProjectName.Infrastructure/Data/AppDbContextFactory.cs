using Fabricdot.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProjectName.Infrastructure.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // Build config
        var config = ConfigurationFactory.Build(new ConfigurationBuilderOptions()
        {
            BasePath = Path.Combine(Directory.GetCurrentDirectory(), "../ProjectName.WebApi"),
            EnvironmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
        });
        var connectionString = config.GetConnectionString("Default");
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        // TODO:use database provider.

        return new AppDbContext(optionsBuilder.Options);
    }
}
