using Npgsql;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class ReadingPostgresTest
{
	[Test]
	public void ShouldParseDatabaseName()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Database = "db"}.ToString();

		connectionString.DatabaseName().Should().Be("db");
	}

	[Test]
	public void ShouldParseDatabaseNameNull()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		connectionString.DatabaseName().Should().Be(null);
	}

	[Test]
	public void ShouldParseDatabaseNameNull2()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Database = null}.ToString();

		connectionString.DatabaseName().Should().Be(null);
	}

	[Test]
	public void ShouldParseServerName()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "server"}.ToString();

		connectionString.ServerName().Should().Be("server");
	}

	[Test]
	public void ShouldParseServerNameNull()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = null, Username = "user"}.ToString();

		connectionString.ServerName().Should().Be(null);
	}
	
	[Test]
	public void ShouldParseApplicationName()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", ApplicationName = "app"}.ToString();

		connectionString.ApplicationName().Should().Be("app");
	}

	[Test]
	public void ShouldParseApplicationNameNull()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		connectionString.ApplicationName().Should().Be(null);
	}

	[Test]
	public void ShouldParsePassword()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Password = "pass"}.ToString();

		connectionString.Password().Should().Be("pass");
	}

	[Test]
	public void ShouldParseUserName()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Username = "user"}.ToString();

		connectionString.UserName().Should().Be("user");
	}

	[Test]
	public void ShouldNotSupportIntegratedSecurity()
	{
		var connectionString = "Host=foo;Database=bar";

		connectionString.ServerName().Should().Be("foo");
		connectionString.DatabaseName().Should().Be("bar");
		connectionString.IntegratedSecurity().Should().Be.False();
	}

	[Test]
	public void ShouldParseConnectionTimeout()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Timeout = 42}.ToString();

		connectionString.ConnectionTimeout().Should().Be(42);
	}
}