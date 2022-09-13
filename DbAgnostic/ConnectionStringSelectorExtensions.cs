using System;

namespace DbAgnostic
{
	public static class ConnectionStringSelectorExtensions
	{
		public static T PickDialect<T>(this string connectionString, T sqlServer, T postgres) =>
			connectionString.ToDbSelector().PickDialect(sqlServer, postgres);

		public static T PickFunc<T>(this string connectionString, Func<T> sqlServer, Func<T> postgres) =>
			connectionString.ToDbSelector().PickFunc(sqlServer, postgres);

		public static void PickAction(this string connectionString, Action sqlServer, Action postgres) =>
			connectionString.ToDbSelector().PickAction(sqlServer, postgres);
	}
}
