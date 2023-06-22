using Fabricdot.Core.Modularity;
using Fabricdot.Domain;
using ProjectName.Domain.Shared;

namespace ProjectName.Domain;

[Requires(typeof(AppNameDomainSharedModule))]
[Requires(typeof(FabricdotDomainModule))]
[Exports]
public class AppNameDomainModule : ModuleBase
{
}
