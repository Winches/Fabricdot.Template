using Dapper;
using Fabricdot.Core.Reflection;
using Fabricdot.Domain.ValueObjects;

namespace ProjectName.Infrastructure.Data.TypeHandlers;

public static class SqlMapperTypeHandlerConfiguration
{
    public static void AddTypeHandlers()
    {
        // register enumeration handlers
        var enumerationType = ReflectionHelper.FindTypes(typeof(Enumeration), AppDomain.CurrentDomain.GetAssemblies());
        enumerationType
            .ForEach(v =>
            {
                var handlerType = typeof(EnumerationTypeHandler<>).MakeGenericType(v);
                var typeHandler = (SqlMapper.ITypeHandler)Activator.CreateInstance(handlerType)!;
                SqlMapper.AddTypeHandler(v, typeHandler);
            });
    }
}
