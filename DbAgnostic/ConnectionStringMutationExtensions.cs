using System.Data.SqlClient;
using Npgsql;

namespace DbAgnostic
{
	public static class ConnectionStringMutationExtensions
	{
		public static string ChangeDatabase(this string connectionString, string newDatabase)
		{
			return new ConnectionStringDbSelector(connectionString).PickFunc(
				() => new SqlConnectionStringBuilder(connectionString) {InitialCatalog = newDatabase}.ToString(),
				() => new NpgsqlConnectionStringBuilder(connectionString) {Database = newDatabase}.ToString());
		}

		public static string PointToMasterDatabase(this string connectionString)
		{
			return new ConnectionStringDbSelector(connectionString).PickFunc(
				() => new SqlConnectionStringBuilder(connectionString) {InitialCatalog = "master"}.ToString(),
				() => new NpgsqlConnectionStringBuilder(connectionString) {Database = "postgres"}.ToString());
		}

		public static string ChangeServer(this string connectionString, string server)
		{
			return new ConnectionStringDbSelector(connectionString).PickFunc(
				() => new SqlConnectionStringBuilder(connectionString) {DataSource = server}.ToString(),
				() => new NpgsqlConnectionStringBuilder(connectionString) {Host = server}.ToString());
		}

		public static string ChangeApplicationName(this string connectionString, string applicationName)
		{
			return new ConnectionStringDbSelector(connectionString).PickFunc(
				() => new SqlConnectionStringBuilder(connectionString) {ApplicationName = applicationName}.ToString(),
				() => new NpgsqlConnectionStringBuilder(connectionString)
					{ApplicationName = applicationName}.ToString());
		}

		public static string SetUserNameAndPassword(this string connectionString, string userName, string password)
		{
			return new ConnectionStringDbSelector(connectionString).PickFunc(
				() =>
				{
					var ret = new SqlConnectionStringBuilder(connectionString) {UserID = userName, Password = password};
					ret.Remove("Integrated security");
					return ret.ToString();
				},
				() =>
				{
					var ret = new NpgsqlConnectionStringBuilder(connectionString)
						{Username = userName, Password = password};
					ret.Remove("Integrated security");
					return ret.ToString();
				}
			);
		}

		public static string SetCredentials(this string connectionString, bool useIntegratedSecurity, string userName, string password)
		{
			return new ConnectionStringDbSelector(connectionString).PickFunc(
				() =>
				{
					if (useIntegratedSecurity)
					{
						var ret = new SqlConnectionStringBuilder(connectionString) {IntegratedSecurity = true};
						ret.Remove("User Id");
						ret.Remove("Password");
						return ret.ToString();
					}

					return SetUserNameAndPassword(connectionString, userName, password);
				},
				() =>
				{
					if (useIntegratedSecurity)
					{
						return new NpgsqlConnectionStringBuilder(connectionString)
							{IntegratedSecurity = true, Username = null, Password = null}.ToString();
					}

					return SetUserNameAndPassword(connectionString, userName, password);
				});
		}
	}
}
