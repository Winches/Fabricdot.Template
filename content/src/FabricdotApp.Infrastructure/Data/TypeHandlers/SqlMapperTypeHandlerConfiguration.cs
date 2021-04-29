using System;
using System.Linq;
using System.Reflection;
using Dapper;
using Fabricdot.Core.Reflection;

namespace FabricdotApp.Infrastructure.Data.TypeHandlers
{
    public static class SqlMapperTypeHandlerConfiguration
    {
        public static void AddTypeHandlers()
        {
            ReflectionHelper.FindTypes(typeof(SqlMapper.ITypeHandler), typeof(EnumerationTypeHandlerBase<>).Assembly)
                .ForEach(v =>
                {
                    var baseType = v.GetTopBaseType();
                    if (baseType.GetGenericTypeDefinition() != typeof(SqlMapper.TypeHandler<>))
                        return;

                    var typeHandler = (SqlMapper.ITypeHandler)Activator.CreateInstance(v);
                    SqlMapper.AddTypeHandler(baseType.GenericTypeArguments.Single(), typeHandler);
                });
        }
    }
}