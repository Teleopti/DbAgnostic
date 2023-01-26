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
	public void ShouldParseServerName()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "server"}.ToString();

		connectionString.ServerName().Should().Be("server");
	}

	[Test]
	public void ShouldParseApplicationName()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", ApplicationName = "app"}.ToString();

		connectionString.ApplicationName().Should().Be("app");
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
}
