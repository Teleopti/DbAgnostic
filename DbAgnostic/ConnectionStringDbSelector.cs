using System;

namespace DbAgnostic;

internal class ConnectionStringDbSelector : IDbSelector
{
	private readonly string _connectionString;

	public ConnectionStringDbSelector(string connectionString)
	{
		_connectionString = connectionString;
	}

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
			DbProviderFactoryDependency.SqlConnectionStringBuilder(_connectionString);
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

	private bool isPostgres()
	{
		try
		{
			DbProviderFactoryDependency.NpgsqlConnectionStringBuilder(_connectionString);
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}
}