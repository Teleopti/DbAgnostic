using Microsoft.Data.SqlClient;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test;

public class SelectorSqlServerTest
{
	[Test]
	public void ShouldSelectString()
	{
		var selector = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString().ToDbSelector();

		var result = selector.PickDialect("sql server", "postgres");

		result.Should().Be("sql server");
	}

	[Test]
	public void ShouldSelectObject()
	{
		var selector = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString().ToDbSelector();

		var sqlserver = new object();
		var postgres = new object();
		var result = selector.PickDialect(sqlserver, postgres);

		result.Should().Be.SameInstanceAs(sqlserver);
	}

	[Test]
	public void ShouldCallFunc()
	{
		var selector = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString().ToDbSelector();

		var sqlserver = false;
		var postgres = false;
		var result = selector.PickFunc(() => { return sqlserver = true; }, () => { return postgres = true; });

		result.Should().Be(true);
		sqlserver.Should().Be(true);
		postgres.Should().Be(false);
	}

	[Test]
	public void ShouldExecute()
	{
		var selector = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString().ToDbSelector();

		var sqlserver = false;
		var postgres = false;
		selector.PickAction(() => { sqlserver = true; }, () => { postgres = true; });

		sqlserver.Should().Be(true);
		postgres.Should().Be(false);
	}
}
