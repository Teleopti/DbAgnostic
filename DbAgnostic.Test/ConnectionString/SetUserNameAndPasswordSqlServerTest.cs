using Microsoft.Data.SqlClient;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class SetUserNameAndPasswordSqlServerTest
{
	[Test]
	public void ShouldSetUserNameAndPassword()
	{
		var connectionString = new SqlConnectionStringBuilder{DataSource = "foo"}.ToString();

		var result = new SqlConnectionStringBuilder(connectionString.SetUserNameAndPassword("user", "pass"));

		result.UserID.Should().Be.EqualTo("user");
		result.Password.Should().Be.EqualTo("pass");
	}

	[Test]
	public void ShouldTurnOffIntegratedSecurity()
	{
		var connectionString = new SqlConnectionStringBuilder{DataSource = "foo", IntegratedSecurity = true}.ToString();

		var result = new SqlConnectionStringBuilder(connectionString.SetUserNameAndPassword("user", "pass"));

		result.IntegratedSecurity.Should().Be.False();
	}

	[Test]
	public void ShouldClearIntegratedSecurity()
	{
		var connectionString = new SqlConnectionStringBuilder{DataSource = "foo", IntegratedSecurity = true}.ToString();

		var result = connectionString.SetUserNameAndPassword("user", "pass");

		result.Should().Not.Contain("Integrated Security");
	}
}
