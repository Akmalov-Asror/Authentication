using System.Data;

namespace Teaching.Common.Data;

public interface IDatabaseConnection : IDisposable
{
    IDbConnection GetConnection();
    string ConnectionName { get; init; }
}
