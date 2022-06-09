using Fabricdot.Core.Modularity;
using Fabricdot.Domain;

namespace FabricdotApp.Domain
{
    [Requires(typeof(FabricdotAppDomainSharedModule))]
    [Requires(typeof(FabricdotDomainModule))]
    [Exports]
    public class FabricdotAppDomainModule : ModuleBase
    {
    }
}