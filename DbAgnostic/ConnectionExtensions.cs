using System.Data.Common;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace DbAgnostic
{
	public static class ConnectionExtensions
	{
		public static DbConnection CreateConnection(this string connectionString)
		{
			var connection = new ConnectionStringDbSelector(connectionString).PickFunc<DbConnection>(
				() => new SqlConnection(),
				() => new NpgsqlConnection());
			connection.ConnectionString = connectionString;
			return connection;
		}
	}
}
