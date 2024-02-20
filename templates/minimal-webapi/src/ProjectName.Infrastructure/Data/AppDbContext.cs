using System.Reflection;
using Fabricdot.Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProjectName.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContextBase(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
