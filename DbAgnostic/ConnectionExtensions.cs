using System.Data.Common;

namespace DbAgnostic;

public static class ConnectionExtensions
{
	public static DbConnection CreateConnection(this string connectionString)
	{
		var connection = new ConnectionStringDbSelector(connectionString).PickFunc(
			() => DbProviderFactoryDependency.SqlServer.CreateConnection(),
			() => DbProviderFactoryDependency.Postgres.CreateConnection());
		connection.ConnectionString = connectionString;
		return connection;
	}
}