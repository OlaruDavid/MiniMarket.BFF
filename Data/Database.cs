using System.Data;
using Npgsql;

namespace MiniMarket.Data;

public static class Database
{
    private static string _connectionString;

    public static void SetConnString(IConfiguration config)
    => _connectionString = config.GetConnectionString("DefaultConnection");

    public static IDbConnection GetConnection()
    => new NpgsqlConnection(_connectionString);
}