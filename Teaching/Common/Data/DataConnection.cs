using Microsoft.Data.SqlClient;
using System.Data;

namespace Teaching.Common.Data;

public class DataConnection : DatabaseConnection, IDataConnection
{
    public override string ConnectionName { get; init; } = "Data";

    public DataConnection(IConfiguration configuration) 
        => configuration.GetSection("DefaultConnection").GetSection(ConnectionName).Bind(_options);

    public override void Dispose() 
        => _connection?.Dispose();
}