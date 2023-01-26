using Microsoft.Data.SqlClient;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class MutationSqlServerTest
{
	[Test]
	public void ShouldChangeDatabase()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", InitialCatalog = "original"}.ToString();

		var result = connectionString.ChangeDatabase("changed");

		new SqlConnectionStringBuilder(result).InitialCatalog.Should().Be("changed");
	}

	[Test]
	public void ShouldPointToMasterDatabase()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", InitialCatalog = "db"}.ToString();

		var result = connectionString.PointToMasterDatabase();

		new SqlConnectionStringBuilder(result).InitialCatalog.Should().Be("master");
	}

	[Test]
	public void ShouldChangeServer()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "server"}.ToString();

		var result = connectionString.ChangeServer("anotherserver");

		new SqlConnectionStringBuilder(result).DataSource.Should().Be("anotherserver");
	}

	[Test]
	public void ShouldChangeApplicationName()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", ApplicationName = "app"}.ToString();

		var result = connectionString.ChangeApplicationName("coolapp");

		new SqlConnectionStringBuilder(result).ApplicationName.Should().Be("coolapp");
	}
}
