using System;

namespace DbAgnostic
{
	public interface IDbSelector
	{
		T PickFunc<T>(Func<T> sqlServer, Func<T> postgres);
	}
}
