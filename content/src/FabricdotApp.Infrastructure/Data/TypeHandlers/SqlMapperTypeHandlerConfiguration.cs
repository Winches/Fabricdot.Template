using System;
using System.Linq;
using Dapper;
using Fabricdot.Common.Core.Reflections;

namespace FabricdotApp.Infrastructure.Data.TypeHandlers
{
    public static class SqlMapperTypeHandlerConfiguration
    {
        public static void AddTypeHandlers()
        {
            Reflection.FindTypes(typeof(SqlMapper.ITypeHandler), typeof(EnumerationTypeHandlerBase<>).Assembly)
                .ForEach(v =>
                {
                    var baseType = v.GetTopBaseType();
                    if (baseType.GetGenericTypeDefinition() != typeof(SqlMapper.TypeHandler<>))
                        return;

                    var typeHandler = (SqlMapper.ITypeHandler) Activator.CreateInstance(v);
                    SqlMapper.AddTypeHandler(baseType.GenericTypeArguments.Single(), typeHandler);
                });
        }
    }
}