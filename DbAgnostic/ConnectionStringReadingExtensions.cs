using System.Data.Common;

namespace DbAgnostic;

public static class ConnectionStringReadingExtensions
{
	public static string DatabaseName(this string connectionString) =>
		connectionString.PickFunc(
			() => DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString).StringValue("Initial Catalog"),
			() => DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString).StringValue("Database")
		);

	public static string ServerName(this string connectionString) =>
		connectionString.PickFunc(
			() => DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString).StringValue("Data Source"),
			() => DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString).StringValue("Host")
		);

	public static string ApplicationName(this string connectionString) =>
		connectionString.PickFunc(
			() =>
			{
				var value = DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString).StringValue("Application Name");
				return ApplicationNameIsNotSet(value) ? null : value;

				// because builder will return a app name even though the connection string does not have one
				static bool ApplicationNameIsNotSet(string applicationName) =>
					string.IsNullOrEmpty(applicationName) ||
					applicationName == ".Net SqlClient Data Provider" ||
					applicationName == "Core .Net SqlClient Data Provider";
			},
			() => DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString).StringValue("Application Name")
		);

	public static string Password(this string connectionString) =>
		connectionString.PickFunc(
			() => DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString).StringValue("Password"),
			() => DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString).StringValue("Password")
		);

	public static string UserName(this string connectionString) =>
		connectionString.PickFunc(
			() => DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString).StringValue("User ID"),
			() => DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString).StringValue("Username")
		);

	public static bool IntegratedSecurity(this string connectionString) =>
		connectionString.PickFunc(
			() => (bool) DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString)["Integrated Security"],
			() => (bool) DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString)["Integrated Security"]
		);

	public static int ConnectionTimeout(this string connectionString) =>
		connectionString.PickFunc(
			() => (int) DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString)["Connect Timeout"],
			() => (int) DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString)["Timeout"]
		);
	
	private static string StringValue(this DbConnectionStringBuilder connectionString, string key)
	{
		var value = (string) connectionString[key];
		if (string.IsNullOrEmpty(value))
			return null;
		return value;
	}
}