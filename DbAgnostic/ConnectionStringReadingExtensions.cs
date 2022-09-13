using System.Data.SqlClient;
using Npgsql;

namespace DbAgnostic
{
	public static class ConnectionStringReadingExtensions
	{
		public static string DatabaseName(this string connectionString)
		{
			return new ConnectionStringDbSelector(connectionString).PickFunc(
				() => new SqlConnectionStringBuilder(connectionString).InitialCatalog,
				() => new NpgsqlConnectionStringBuilder(connectionString).Database);
		}

		public static string ServerName(this string connectionString)
		{
			return new ConnectionStringDbSelector(connectionString).PickFunc(
				() => new SqlConnectionStringBuilder(connectionString).DataSource,
				() => new NpgsqlConnectionStringBuilder(connectionString).Host);
		}

		public static string ApplicationName(this string connectionString)
		{
			return new ConnectionStringDbSelector(connectionString).PickFunc(
				() => new SqlConnectionStringBuilder(connectionString).ApplicationName,
				() => new NpgsqlConnectionStringBuilder(connectionString).ApplicationName);
		}

		public static string Password(this string connectionString)
		{
			return new ConnectionStringDbSelector(connectionString).PickFunc(
				() => new SqlConnectionStringBuilder(connectionString).Password,
				() => new NpgsqlConnectionStringBuilder(connectionString).Password
			);
		}

		public static string UserName(this string connectionString)
		{
			return new ConnectionStringDbSelector(connectionString).PickFunc(
				() => new SqlConnectionStringBuilder(connectionString).UserID,
				() => new NpgsqlConnectionStringBuilder(connectionString).Username
			);
		}

		public static bool IntegratedSecurity(this string connectionString)
		{
			return new ConnectionStringDbSelector(connectionString).PickFunc(
				() => new SqlConnectionStringBuilder(connectionString).IntegratedSecurity,
				() => new NpgsqlConnectionStringBuilder(connectionString).IntegratedSecurity);
		}
	}
}
