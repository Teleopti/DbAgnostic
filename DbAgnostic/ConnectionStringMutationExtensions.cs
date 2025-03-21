namespace DbAgnostic;

public static class ConnectionStringMutationExtensions
{
	public static string ChangeDatabase(this string connectionString, string newDatabase)
	{
		return connectionString.PickFunc(
			() =>
			{
				var x = DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString);
				x["Initial Catalog"] = newDatabase;
				return x.ToString();
			},
			() =>
			{
				var x = DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString);
				x["Database"] = newDatabase;
				return x.ToString();
			});
	}

	public static string RemoveDatabase(this string connectionString) =>
		connectionString.ChangeDatabase(null);

	public static string PointToMasterDatabase(this string connectionString)
	{
		return connectionString.PickFunc(
			() => connectionString.ChangeDatabase("master"),
			() => connectionString.ChangeDatabase("postgres"));
	}

	public static string ChangeServer(this string connectionString, string server)
	{
		return connectionString.PickFunc(
			() =>
			{
				var x = DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString);
				x["Data Source"] = server;
				return x.ToString();
			},
			() =>
			{
				var x = DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString);
				x["Host"] = server;
				return x.ToString();
			});
	}

	public static string RemoveServer(this string connectionString) =>
		connectionString.ChangeServer(null);

	public static string ChangeApplicationName(this string connectionString, string applicationName)
	{
		return connectionString.PickFunc(
			() =>
			{
				var x = DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString);
				x["Application Name"] = applicationName;
				return x.ToString();
			},
			() =>
			{
				var x = DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString);
				x["Application Name"] = applicationName;
				return x.ToString();
			});
	}

	public static string RemoveApplicationName(this string connectionString) =>
		connectionString.ChangeApplicationName(null);

	public static string SetCredentials(this string connectionString, string userName, string password) =>
		connectionString.PickFunc(
			() =>
			{
				var x = DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString);
				x["User ID"] = userName;
				x["Password"] = password;
				x["Integrated Security"] = null;
				return x.ToString();
			},
			() =>
			{
				var x = DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString);
				x["Username"] = userName;
				x["Password"] = password;
				return x.ToString();
			}
		);
	
	public static string SetIntegratedSecurity(this string connectionString) =>
		connectionString.PickFunc(
			() =>
			{				
				var x = DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString);
				x["User ID"] = null;
				x["Password"] = null;
				x["Integrated Security"] = true;
				return x.ToString();
			},
			() =>
			{
				var x = DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString);
				x["Username"] = null;
				x["Password"] = null;
				return x.ToString();
			});

	public static string RemoveCredentials(this string connectionString) =>
		SetCredentials(connectionString, null, null);

	public static string SetConnectionTimeout(this string connectionString, int seconds) =>
		connectionString.PickFunc(
			() =>
			{
				var x = DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString);
				x["Connect Timeout"] = seconds;
				return x.ToString();
			},
			() =>
			{
				var x = DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString);
				x["Timeout"] = seconds;
				return x.ToString();
			});
}