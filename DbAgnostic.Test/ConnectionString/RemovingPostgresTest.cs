using Npgsql;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class RemovingPostgresTest
{
    [Test]
    public void ShouldRemoveDatabase()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Database = "db"}.ToString();

        var result = connectionString.ChangeDatabase(null);

        result.Should().Be("Host=foo");
    }

    [Test]
    public void ShouldRemoveDatabase2()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Database = "db"}.ToString();

        var result = connectionString.RemoveDatabase();

        result.Should().Be("Host=foo");
    }

    [Test]
    public void ShouldRemoveServer()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Database = "db", Host = "server"}.ToString();

        var result = connectionString.ChangeServer(null);

        result.Should().Be("Database=db");
    }

    [Test]
    public void ShouldRemoveServer2()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Database = "db", Host = "server"}.ToString();

        var result = connectionString.RemoveServer();

        result.Should().Be("Database=db");
    }

    [Test]
    public void ShouldRemoveApplicationName()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", ApplicationName = "app"}.ToString();

        var result = connectionString.ChangeApplicationName(null);

        result.Should().Be("Host=foo");
    }

    [Test]
    public void ShouldRemoveApplicationName2()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", ApplicationName = "app"}.ToString();

        var result = connectionString.RemoveApplicationName();

        result.Should().Be("Host=foo");
    }
}