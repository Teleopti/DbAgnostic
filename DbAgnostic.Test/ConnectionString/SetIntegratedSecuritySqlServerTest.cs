using System.Data.SqlClient;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class SetIntegratedSecuritySqlServerTest
{
    [Test]
    public void ShouldSetIntegratedSecurity()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

        var result = connectionString.SetIntegratedSecurity();

        result.IntegratedSecurity().Should().Be.True();
    }
    
    [Test]
    public void ShouldRemoveUserNameAndPassword()
    {
         var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", UserID = "user", Password = "pass"}.ToString();
    
         var result = connectionString.SetIntegratedSecurity();
    
         result.Should().Not.Contain("Password");
         result.Should().Not.Contain("User Id");
    }
}