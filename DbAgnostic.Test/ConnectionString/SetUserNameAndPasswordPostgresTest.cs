using Npgsql;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class SetUserNameAndPasswordPostgresTest
{
	[Test]
	public void ShouldSetUserNameAndPassword()
	{
		var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo"}.ToString();

		var result = new NpgsqlConnectionStringBuilder(connectionString.SetUserNameAndPassword("user", "pass"));

		result.Username.Should().Be.EqualTo("user");
		result.Password.Should().Be.EqualTo("pass");
	}

	[Test]
	public void ShouldTurnOffIntegratedSecurity()
	{
		var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo", IntegratedSecurity = true}.ToString();

		var result = new NpgsqlConnectionStringBuilder(connectionString.SetUserNameAndPassword("user", "pass"));

		result.IntegratedSecurity.Should().Be.False();
	}

	[Test]
	public void ShouldClearIntegratedSecurity()
	{
		var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo", IntegratedSecurity = true}.ToString();

		var result = connectionString.SetUserNameAndPassword("user", "pass");

		result.Should().Not.Contain("Integrated Security");
	}
}
