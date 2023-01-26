using Microsoft.Data.SqlClient;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test;

public class SelectorDbConnectionSqlServerTest
{
	[Test]
	public void ShouldSelectString()
	{
		var connection = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString().CreateConnection();

		var result = connection.PickDialect("sql server", "postgres");

		result.Should().Be("sql server");
	}

	[Test]
	public void ShouldSelectObject()
	{
		var connection = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString().CreateConnection();

		var sqlserver = new object();
		var postgres = new object();
		var result = connection.PickDialect(sqlserver, postgres);

		result.Should().Be.SameInstanceAs(sqlserver);
	}

	[Test]
	public void ShouldCallFunc()
	{
		var connection = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString().CreateConnection();

		var sqlserver = false;
		var postgres = false;
		var result = connection.PickFunc(() => { return sqlserver = true; }, () => { return postgres = true; });

		result.Should().Be(true);
		sqlserver.Should().Be(true);
		postgres.Should().Be(false);
	}

	[Test]
	public void ShouldExecute()
	{
		var connection = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString().CreateConnection();

		var sqlserver = false;
		var postgres = false;
		connection.PickAction(() => { sqlserver = true; }, () => { postgres = true; });

		sqlserver.Should().Be(true);
		postgres.Should().Be(false);
	}
}
