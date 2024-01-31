using System;
using System.Data.Common;
using System.Linq;

namespace DbAgnostic;

internal static class DbProviderFactoryDependency
{
	private static readonly Lazy<DbProviderFactory> _sqlServer = new(() =>
	{
		var sqlServerTypes = new[]
		{
			"Microsoft.Data.SqlClient.SqlClientFactory, Microsoft.Data.SqlClient",
			// // Available in the .NET Framework GAC, requires Version + Culture + PublicKeyToken to be explicitly specified
			// "System.Data.SqlClient.SqlClientFactory, System.Data, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089",
			"System.Data.SqlClient.SqlClientFactory, System.Data.SqlClient",
		};
		return GetProviderFactory(sqlServerTypes);
	});

	private static readonly Lazy<DbProviderFactory> _postgres = new(() => GetProviderFactory("Npgsql.NpgsqlFactory, Npgsql"));

	public static DbConnectionStringBuilder SqlConnectionStringBuilder(string connectionString)
	{
		var x = SqlServer.CreateConnectionStringBuilder();
		x.ConnectionString = connectionString;
		return x;
	}
	
	public static DbConnectionStringBuilder NpgsqlConnectionStringBuilder(string connectionString)
	{
		var x = Postgres.CreateConnectionStringBuilder();
		x.ConnectionString = connectionString;
		return x;
	}

	public static DbProviderFactory SqlServer => _sqlServer.Value;
	public static DbProviderFactory Postgres => _postgres.Value;
	
	private static DbProviderFactory GetProviderFactory(params string[] types)
	{
		var result = types
			.Select(x => Type.GetType(x, throwOnError: false))
			.Where(x => x != null)
			.Select(x => x.GetField("Instance")?.GetValue(null))
			.OfType<DbProviderFactory>()
			.SingleOrDefault();
		if (result == null)
			throw new Exception($"DbProviderFactory {string.Join(" or ", types)} not found. Reference need to be added in client.");
		return result;
	}
}