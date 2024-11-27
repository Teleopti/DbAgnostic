using System.Data.SqlClient;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class SetCredentialsSqlServerTest
{
    [Test]
    public void ShouldRemoveCredentials2()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", UserID = "u", Password = "p"}.ToString();

        var result = connectionString.RemoveCredentials();

        result.IntegratedSecurity().Should().Be.False();
        result.UserName().Should().Be.Null();
        result.Password().Should().Be.Null();
    }
    
    [Test]
    public void ShouldRemoveCredentials3()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", IntegratedSecurity = true}.ToString();

        var result = connectionString.RemoveCredentials();

        result.IntegratedSecurity().Should().Be.False();
        result.UserName().Should().Be.Null();
        result.Password().Should().Be.Null();
    }
}