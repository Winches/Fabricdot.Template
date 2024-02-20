using System.Data;
using Fabricdot.Infrastructure.Data;

namespace ProjectName.Infrastructure.Data;

public class DefaultSqlConnectionFactory(string connectionString) : SqlConnectionFactory(connectionString)
{
    protected override IDbConnection CreateConnection(string connectionString)
    {
        // Create db connection.
        throw new NotImplementedException();
    }
}
