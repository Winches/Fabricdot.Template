using Fabricdot.Core.Modularity;
using Fabricdot.Domain;

namespace ProjectName.Domain.Shared;

[Requires(typeof(FabricdotDomainModule))]
[Exports]
public class AppNameDomainSharedModule : ModuleBase
{
}
