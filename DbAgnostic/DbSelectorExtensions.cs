using System;
using System.Data.Common;

namespace DbAgnostic;

public static class DbSelectorExtensions
{
	public static IDbSelector ToDbSelector(this string connectionString) =>
		new ConnectionStringDbSelector(connectionString);

	public static IDbSelector ToDbSelector(this DbConnection connection) =>
		new ConnectionStringDbSelector(connection.ConnectionString);

	public static T PickDialect<T>(this IDbSelector dbSelector, T sqlServer, T postgres) =>
		dbSelector.PickFunc(() => sqlServer, () => postgres);

	public static void PickAction(this IDbSelector dbSelector, Action sqlServer, Action postgres)
	{
		dbSelector.PickFunc(() =>
		{
			sqlServer?.Invoke();
			return true;
		}, () =>
		{
			postgres?.Invoke();
			return true;
		});
	}
}