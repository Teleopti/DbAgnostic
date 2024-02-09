using System;

namespace DbAgnostic;

internal class ConnectionStringDbSelector(string connectionString) : IDbSelector
{
	public T PickFunc<T>(Func<T> sqlServer, Func<T> postgres)
	{
		if (isSqlServer())
			return sqlServer();
		if (isPostgres())
			return postgres();
		return default;
	}

	private bool isSqlServer()
	{
		try
		{
			DbProviderFactoryDependency.SqlConnectionStringBuilder(connectionString);
			return true;
		}
		catch (ArgumentException)
		{
			return false;
		}
	}

	private bool isPostgres()
	{
		try
		{
			DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(connectionString);
			return true;
		}
		catch (ArgumentException)
		{
			return false;
		}
	}
}