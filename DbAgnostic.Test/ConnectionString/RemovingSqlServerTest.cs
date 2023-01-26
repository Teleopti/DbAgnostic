using System.Data.SqlClient;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class RemovingSqlServerTest
{
    [Test]
    public void ShouldRemoveDatabase()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", InitialCatalog = "db"}.ToString();

        var result = connectionString.ChangeDatabase(null);

        result.Should().Be("Data Source=foo");
    }

    [Test]
    public void ShouldRemoveDatabase2()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", InitialCatalog = "db"}.ToString();

        var result = connectionString.RemoveDatabase();

        result.Should().Be("Data Source=foo");
    }

    [Test]
    public void ShouldRemoveServer()
    {
        var connectionString = new SqlConnectionStringBuilder {InitialCatalog = "db", DataSource = "server"}.ToString();

        var result = connectionString.ChangeServer(null);

        result.Should().Be("Initial Catalog=db");
    }

    [Test]
    public void ShouldRemoveServer2()
    {
        var connectionString = new SqlConnectionStringBuilder {InitialCatalog = "db", DataSource = "server"}.ToString();

        var result = connectionString.RemoveServer();

        result.Should().Be("Initial Catalog=db");
    }

    [Test]
    public void ShouldRemoveApplicationName()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", ApplicationName = "app"}.ToString();

        var result = connectionString.ChangeApplicationName(null);

        result.Should().Be("Data Source=foo");
    }

    [Test]
    public void ShouldRemoveApplicationName2()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", ApplicationName = "app"}.ToString();

        var result = connectionString.RemoveApplicationName();

        result.Should().Be("Data Source=foo");
    }
}