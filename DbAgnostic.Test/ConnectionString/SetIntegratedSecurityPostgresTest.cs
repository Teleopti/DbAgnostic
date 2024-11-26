using System.Data.SqlClient;
using Npgsql;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class SetIntegratedSecurityPostgresTest
{
    [Test]
    public void ShouldNotSetIntegratedSecurityAsItIsNotSupported()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

        var result = connectionString.SetIntegratedSecurity();

        result.IntegratedSecurity().Should().Be.False();
    }
    
    [Test]
    public void ShouldRemoveUserNameAndPassword()
    {
        var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo", Username = "user", Password = "pass"}.ToString();
    
        var result = connectionString.SetIntegratedSecurity();
    
        result.Should().Not.Contain("Password");
        result.Should().Not.Contain("User Id");
    }
}