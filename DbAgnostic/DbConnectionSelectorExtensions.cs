using System;
using System.Data.Common;

namespace DbAgnostic;

public static class DbConnectionSelectorExtensions
{
	public static T PickDialect<T>(this DbConnection connection, T sqlServer, T postgres) =>
		connection.ToDbSelector().PickDialect(sqlServer, postgres);

	public static T PickFunc<T>(this DbConnection connection, Func<T> sqlServer, Func<T> postgres) =>
		connection.ToDbSelector().PickFunc(sqlServer, postgres);

	public static void PickAction(this DbConnection connection, Action sqlServer, Action postgres) =>
		connection.ToDbSelector().PickAction(sqlServer, postgres);
}