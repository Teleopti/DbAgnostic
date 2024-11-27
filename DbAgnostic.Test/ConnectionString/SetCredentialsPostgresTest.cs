using Npgsql;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class SetCredentialsPostgresTest
{
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