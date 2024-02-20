using Fabricdot.Core.DependencyInjection;
using Fabricdot.Infrastructure.Uow.Abstractions;

namespace ProjectName.Infrastructure.Data;

internal class DataBuilder(IUnitOfWorkManager unitOfWorkManager) : ITransientDependency
{
    private readonly IUnitOfWorkManager _unitOfWorkManager = unitOfWorkManager;

    public virtual async Task SeedAsync()
    {
        using var uow = _unitOfWorkManager.Begin();
        // seed data
        await uow.CommitChangesAsync();
    }
}
