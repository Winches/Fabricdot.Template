using System;
using System.Data;
using Fabricdot.Infrastructure.Data;

namespace FabricdotApp.Infrastructure.Data
{
    public class DefaultSqlConnectionFactory : SqlConnectionFactory
    {
        public DefaultSqlConnectionFactory(string connectionString) : base(connectionString)
        {
        }

        protected override IDbConnection CreateConnection(string connectionString)
        {
            // Create db connection.
            throw new NotImplementedException();
        }
    }
}