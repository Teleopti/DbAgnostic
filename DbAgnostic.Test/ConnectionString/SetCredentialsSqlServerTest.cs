using System.Data.SqlClient;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class SetCredentialsSqlServerTest
{
    [Test]
    public void ShouldSetUserNameAndPassword()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

        var result = new SqlConnectionStringBuilder(connectionString.SetCredentials(false, "user", "pass"));

        result.UserID.Should().Be.EqualTo("user");
        result.Password.Should().Be.EqualTo("pass");
    }

    [Test]
    public void ShouldTurnOffIntegratedSecurity()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", IntegratedSecurity = true}.ToString();

        var result = new SqlConnectionStringBuilder(connectionString.SetCredentials(false, "user", "pass"));

        result.IntegratedSecurity.Should().Be.False();
    }

    [Test]
    public void ShouldClearIntegratedSecurity()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", IntegratedSecurity = true}.ToString();

        var result = connectionString.SetCredentials(false, "user", "pass");

        result.Should().Not.Contain("Integrated Security");
    }

    [Test]
    public void ShouldSetIntegratedSecurity()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

        var result = new SqlConnectionStringBuilder(connectionString.SetCredentials(true, null, null));

        result.IntegratedSecurity.Should().Be.True();
    }

    [Test]
    public void ShouldClearUserNameAndPassword()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", UserID = "user", Password = "pass"}.ToString();

        var result = new SqlConnectionStringBuilder(connectionString.SetCredentials(true, null, null));

        result.UserID.Should().Be.Empty();
        result.Password.Should().Be.Empty();
    }

    [Test]
    public void ShouldRemoveUserNameAndPassword()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", UserID = "user", Password = "pass"}.ToString();

        var result = connectionString.SetCredentials(true, null, null);

        result.Should().Not.Contain("Password");
        result.Should().Not.Contain("User Id");
    }

    [Test]
    public void ShouldCopyCredentials()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();
        var connectionString2 = new SqlConnectionStringBuilder {DataSource = "bar", UserID = "u", Password = "p"}.ToString();

        var result = connectionString.SetCredentials(connectionString2);

        result.ServerName().Should().Be("foo");
        result.IntegratedSecurity().Should().Be.False();
        result.UserName().Should().Be("u");
        result.Password().Should().Be("p");
    }

    [Test]
    public void ShouldCopyCredentials2()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", UserID = "u", Password = "p"}.ToString();
        var connectionString2 = new SqlConnectionStringBuilder {DataSource = "bar", IntegratedSecurity = true}.ToString();

        var result = connectionString.SetCredentials(connectionString2);

        result.ServerName().Should().Be("foo");
        result.IntegratedSecurity().Should().Be.True();
        // these cant be null for some reason... slight incompat with postgres.
        // probably need to be fixed some day, some how
        result.UserName().Should().Be("");
        result.Password().Should().Be("");
    }
    
    [Test]
    public void ShouldRemoveCredentials()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", UserID = "u", Password = "p"}.ToString();

        var result = connectionString.SetCredentials(false, null, null);

        result.IntegratedSecurity().Should().Be.False();
        result.UserName().Should().Be("");
        result.Password().Should().Be("");
    }

    [Test]
    public void ShouldRemoveCredentials2()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", UserID = "u", Password = "p"}.ToString();

        var result = connectionString.RemoveCredentials();

        result.IntegratedSecurity().Should().Be.False();
        result.UserName().Should().Be("");
        result.Password().Should().Be("");
    }
    
    [Test]
    public void ShouldRemoveCredentials3()
    {
        var connectionString = new SqlConnectionStringBuilder {DataSource = "foo", IntegratedSecurity = true}.ToString();

        var result = connectionString.RemoveCredentials();

        result.IntegratedSecurity().Should().Be.False();
        result.UserName().Should().Be("");
        result.Password().Should().Be("");
    }
}