using System.Data.SqlClient;
using Npgsql;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test;

public class SelectorConnectionStringPostgresTest
{
	[Test]
	public void ShouldSelectString()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		var result = connectionString.PickDialect("sql server", "postgres");

		result.Should().Be("postgres");
	}

	[Test]
	public void ShouldSelectObject()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		var sqlserver = new object();
		var postgres = new object();
		var result = connectionString.PickDialect(sqlserver, postgres);

		result.Should().Be.SameInstanceAs(postgres);
	}

	[Test]
	public void ShouldCallFunc()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		var sqlserver = false;
		var postgres = false;
		var result = connectionString.PickFunc(() => { return sqlserver = true; }, () => { return postgres = true; });

		result.Should().Be(true);
		sqlserver.Should().Be(false);
		postgres.Should().Be(true);
	}

	[Test]
	public void ShouldExecute()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		var sqlserver = false;
		var postgres = false;
		connectionString.PickAction(() => { sqlserver = true; }, () => { postgres = true; });

		sqlserver.Should().Be(false);
		postgres.Should().Be(true);
	}
}
