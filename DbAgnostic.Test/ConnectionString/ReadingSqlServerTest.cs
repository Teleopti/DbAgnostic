using System.Data.SqlClient;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class ReadingSqlServerTest
{
	[Test]
	public void ShouldParseDatabaseName()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", InitialCatalog = "db"}.ToString();

		connectionString.DatabaseName().Should().Be("db");
	}
	
	[Test]
	public void ShouldParseDatabaseNameNull()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

		connectionString.DatabaseName().Should().Be(null);
	}
	
	[Test]
	public void ShouldParseServerName()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "server"}.ToString();

		connectionString.ServerName().Should().Be("server");
	}

	[Test]
	public void ShouldParseServerNameNull()
	{
		var connectionString = new SqlConnectionStringBuilder {UserID = "user"}.ToString();

		connectionString.ServerName().Should().Be(null);
	}
	
	[Test]
	public void ShouldParseApplicationName()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", ApplicationName = "app"}.ToString();

		connectionString.ApplicationName().Should().Be("app");
	}

	[Test]
	public void ShouldParseApplicationNameNull()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

		connectionString.ApplicationName().Should().Be(null);
	}

	[Test]
	public void ShouldParsePassword()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", Password = "pass"}.ToString();

		connectionString.Password().Should().Be("pass");
	}

	[Test]
	public void ShouldParseUserName()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", UserID = "user"}.ToString();

		connectionString.UserName().Should().Be("user");
	}

	[Test]
	public void ShouldParseIntegratedSecurity()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", IntegratedSecurity = true}.ToString();

		connectionString.IntegratedSecurity().Should().Be(true);
	}

	[Test]
	public void ShouldParseConnectionTimeout()
	{
		var connectionString = new SqlConnectionStringBuilder {ConnectTimeout = 47}.ToString();

		connectionString.ConnectionTimeout().Should().Be(47);
	}
}