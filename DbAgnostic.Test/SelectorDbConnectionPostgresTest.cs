using Npgsql;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test;

public class SelectorDbConnectionPostgresTest
{
	[Test]
	public void ShouldSelectString()
	{
		var connection = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().CreateConnection();

		var result = connection.PickDialect("sql server", "postgres");

		result.Should().Be("postgres");
	}

	[Test]
	public void ShouldSelectObject()
	{
		var connection = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().CreateConnection();

		var sqlserver = new object();
		var postgres = new object();
		var result = connection.PickDialect(sqlserver, postgres);

		result.Should().Be.SameInstanceAs(postgres);
	}

	[Test]
	public void ShouldCallFunc()
	{
		var connection = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().CreateConnection();

		var sqlserver = false;
		var postgres = false;
		var result = connection.PickFunc(() => { return sqlserver = true; }, () => { return postgres = true; });

		result.Should().Be(true);
		sqlserver.Should().Be(false);
		postgres.Should().Be(true);
	}

	[Test]
	public void ShouldExecute()
	{
		var connection = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().CreateConnection();

		var sqlserver = false;
		var postgres = false;
		connection.PickAction(() => { sqlserver = true; }, () => { postgres = true; });

		sqlserver.Should().Be(false);
		postgres.Should().Be(true);
	}
}
