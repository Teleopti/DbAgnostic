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
    public void ShouldNotSetIntegratedSecurity()
    {
        var connectionString = "Host=foo";

        var result = connectionString.SetCredentials(false, "user", "pass");

        result.ServerName().Should().Be("foo");
        result.IntegratedSecurity().Should().Be.False();
        result.Should().Not.Contain("Integrated Security");
    }

    [Test]
    public void ShouldIgnoreIntegratedSecurity()
    {
        var connectionString = "Host=foo;Username=u";

        var result = connectionString.SetCredentials(true, null, null);

        result.ServerName().Should().Be("foo");
        result.UserName().Should().Be(null);
        result.IntegratedSecurity().Should().Be.False();
        result.Should().Not.Contain("Integrated Security");
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
        var target = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();
        var source = new NpgsqlConnectionStringBuilder {Host = "bar", Username = "u", Password = "p"}.ToString();

        var result = target.SetCredentials(source);

        result.ServerName().Should().Be("foo");
        result.IntegratedSecurity().Should().Be.False();
        result.UserName().Should().Be("u");
        result.Password().Should().Be("p");
    }

    [Test]
    public void ShouldCopyCredentials2()
    {
        var target = "Host=foo;Username=u;Password=p";
        var source = "Host=foo;";

        var result = target.SetCredentials(source);

        result.ServerName().Should().Be("foo");
        result.IntegratedSecurity().Should().Be.False();
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
        var connectionString = "Host=foo;Integrated Security=true";

        var result = connectionString.RemoveCredentials();

        result.IntegratedSecurity().Should().Be.False();
        result.UserName().Should().Be.Null();
        result.Password().Should().Be.Null();
    }
}