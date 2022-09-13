using Npgsql;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test;

public class SelectorPostgresTest
{
	[Test]
	public void ShouldSelectString()
	{
		var selector = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().ToDbSelector();

		var result = selector.PickDialect("sql server", "postgres");

		result.Should().Be("postgres");
	}

	[Test]
	public void ShouldSelectObject()
	{
		var selector = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().ToDbSelector();

		var sqlserver = new object();
		var postgres = new object();
		var result = selector.PickDialect(sqlserver, postgres);

		result.Should().Be.SameInstanceAs(postgres);
	}

	[Test]
	public void ShouldCallFunc()
	{
		var selector = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().ToDbSelector();

		var sqlserver = false;
		var postgres = false;
		var result = selector.PickFunc(() => { return sqlserver = true; }, () => { return postgres = true; });

		result.Should().Be(true);
		postgres.Should().Be(true);
		sqlserver.Should().Be(false);
	}

	[Test]
	public void ShouldExecute()
	{
		var selector = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString().ToDbSelector();

		var sqlserver = false;
		var postgres = false;
		selector.PickAction(() => { sqlserver = true; }, () => { postgres = true; });

		postgres.Should().Be(true);
		sqlserver.Should().Be(false);
	}
}
