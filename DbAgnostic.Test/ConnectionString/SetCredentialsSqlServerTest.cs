using System.Data.SqlClient;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class SetCredentialsSqlServerTest
{
    [Test]
    public void ShouldSetUserNameAndPassword()
    {
        var connectionString = new SqlConnectionStringBuilder{DataSource = "foo"}.ToString();

        var result = new SqlConnectionStringBuilder(connectionString.SetCredentials("user", "pass"));

        result.UserID.Should().Be.EqualTo("user");
        result.Password.Should().Be.EqualTo("pass");
    }

    [Test]
    public void ShouldTurnOffIntegratedSecurity()
    {
        var connectionString = new SqlConnectionStringBuilder{DataSource = "foo", IntegratedSecurity = true}.ToString();

        var result = new SqlConnectionStringBuilder(connectionString.SetCredentials("user", "pass"));

        result.IntegratedSecurity.Should().Be.False();
    }

    [Test]
    public void ShouldClearIntegratedSecurity()
    {
        var connectionString = new SqlConnectionStringBuilder{DataSource = "foo", IntegratedSecurity = true}.ToString();

        var result = connectionString.SetCredentials("user", "pass");

        result.Should().Not.Contain("Integrated Security");
    }
	
    [Test]
    public void ShouldRemoveUserNameAndPassword()
    {
        var connectionString = new SqlConnectionStringBuilder{DataSource = "foo", UserID = "u", Password = "p"}.ToString();

        var result = connectionString.SetCredentials(null, null);

        result.Should().Not.Contain("User ID");
        result.Should().Not.Contain("Password");
    }
    
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