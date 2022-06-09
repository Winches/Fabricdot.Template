using Fabricdot.Core.Modularity;
using Fabricdot.Domain;

namespace FabricdotApp.Domain
{
    [Requires(typeof(FabricdotDomainModule))]
    [Exports]
    public class FabricdotAppDomainSharedModule : ModuleBase
    {
    }
}