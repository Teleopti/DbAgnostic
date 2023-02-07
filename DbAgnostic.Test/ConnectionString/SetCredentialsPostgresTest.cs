using Npgsql;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class SetCredentialsPostgresTest
{
    [Test]
    public void ShouldSetUserNameAndPassword()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

        var result = new NpgsqlConnectionStringBuilder(connectionString.SetCredentials(false, "user", "pass"));

        result.Username.Should().Be.EqualTo("user");
        result.Password.Should().Be.EqualTo("pass");
    }

    [Test]
    public void ShouldTurnOffIntegratedSecurity()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", IntegratedSecurity = true}.ToString();

        var result = new NpgsqlConnectionStringBuilder(connectionString.SetCredentials(false, "user", "pass"));

        result.IntegratedSecurity.Should().Be.False();
    }

    [Test]
    public void ShouldClearIntegratedSecurity()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", IntegratedSecurity = true}.ToString();

        var result = connectionString.SetCredentials(false, "user", "pass");

        result.Should().Not.Contain("Integrated Security");
    }

    [Test]
    public void ShouldSetIntegratedSecurity()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

        var result = new NpgsqlConnectionStringBuilder(connectionString.SetCredentials(true, null, null));

        result.IntegratedSecurity.Should().Be.True();
    }

    [Test]
    public void ShouldClearUserNameAndPassword()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Username = "user", Password = "pass"}.ToString();

        var result = new NpgsqlConnectionStringBuilder(connectionString.SetCredentials(true, null, null));

        result.Username.Should().Be.Null();
        result.Password.Should().Be.Null();
    }

    [Test]
    public void ShouldCopyCredentials()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();
        var connectionString2 = new NpgsqlConnectionStringBuilder {Host = "bar", Username = "u", Password = "p"}.ToString();

        var result = connectionString.SetCredentials(connectionString2);

        result.ServerName().Should().Be("foo");
        result.IntegratedSecurity().Should().Be.False();
        result.UserName().Should().Be("u");
        result.Password().Should().Be("p");
    }

    [Test]
    public void ShouldCopyCredentials2()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Username = "u", Password = "p"}.ToString();
        var connectionString2 = new NpgsqlConnectionStringBuilder {Host = "bar", IntegratedSecurity = true}.ToString();

        var result = connectionString.SetCredentials(connectionString2);

        result.ServerName().Should().Be("foo");
        result.IntegratedSecurity().Should().Be.True();
        result.UserName().Should().Be(null);
        result.Password().Should().Be(null);
    }

    [Test]
    public void ShouldRemoveCredentials()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Username = "u", Password = "p"}.ToString();

        var result = connectionString.SetCredentials(false, null, null);

        result.IntegratedSecurity().Should().Be.False();
        result.UserName().Should().Be.Null();
        result.Password().Should().Be.Null();
    }

    [Test]
    public void ShouldRemoveCredentials2()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Username = "u", Password = "p"}.ToString();

        var result = connectionString.RemoveCredentials();

        result.IntegratedSecurity().Should().Be.False();
        result.UserName().Should().Be.Null();
        result.Password().Should().Be.Null();
    }
    
    [Test]
    public void ShouldRemoveCredentials3()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", IntegratedSecurity = true}.ToString();

        var result = connectionString.RemoveCredentials();

        result.IntegratedSecurity().Should().Be.False();
        result.UserName().Should().Be.Null();
        result.Password().Should().Be.Null();
    }
}