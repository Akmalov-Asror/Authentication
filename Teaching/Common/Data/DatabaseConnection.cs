using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Pomelo.EntityFrameworkCore.MySql.Internal;
using System.Data;
using Teaching.Common.Helpers;

namespace Teaching.Common.Data;

public abstract class DatabaseConnection : IDatabaseConnection, IDisposable
{
    public abstract string ConnectionName { get; init; }
    protected readonly SqlOptions _options = new();
    protected SqliteConnection? _connection;

    public abstract void Dispose();

    public virtual IDbConnection GetConnection()
    {
        if (_connection == null || _connection.State == ConnectionState.Broken)
            _connection = new SqliteConnection(_options.ConnectionString);
        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
        }
        return _connection;
    }
}
