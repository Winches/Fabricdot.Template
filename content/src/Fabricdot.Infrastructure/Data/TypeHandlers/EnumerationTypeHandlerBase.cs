using System.Data;
using Dapper;
using Fabricdot.Domain.Core.ValueObjects;

namespace Fabricdot.Infrastructure.Data.TypeHandlers
{
    public abstract class EnumerationTypeHandlerBase<T> : SqlMapper.TypeHandler<T> where T : Enumeration
    {
        /// <inheritdoc />
        public override void SetValue(IDbDataParameter parameter, T value)
        {
            parameter.Value = value.Value;
            parameter.DbType = DbType.Int32;
        }

        /// <inheritdoc />
        public override T Parse(object value)
        {
            return Enumeration.FromValue<T>((int)value);
        }
    }
}