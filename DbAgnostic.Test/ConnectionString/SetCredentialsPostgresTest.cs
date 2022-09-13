using Npgsql;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test.ConnectionString;

public class SetCredentialsPostgresTest
{
	[Test]
	public void ShouldSetUserNameAndPassword()
	{
		var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo"}.ToString();

		var result = new NpgsqlConnectionStringBuilder(connectionString.SetCredentials(false, "user", "pass"));

		result.Username.Should().Be.EqualTo("user");
		result.Password.Should().Be.EqualTo("pass");
	}

	[Test]
	public void ShouldTurnOffIntegratedSecurity()
	{
		var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo", IntegratedSecurity = true}.ToString();

		var result = new NpgsqlConnectionStringBuilder(connectionString.SetCredentials(false, "user", "pass"));

		result.IntegratedSecurity.Should().Be.False();
	}

	[Test]
	public void ShouldClearIntegratedSecurity()
	{
		var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo", IntegratedSecurity = true}.ToString();

		var result = connectionString.SetCredentials(false, "user", "pass");

		result.Should().Not.Contain("Integrated Security");
	}

	[Test]
	public void ShouldSetIntegratedSecurity()
	{
		var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo"}.ToString();

		var result = new NpgsqlConnectionStringBuilder(connectionString.SetCredentials(true, null, null));

		result.IntegratedSecurity.Should().Be.True();
	}

	[Test]
	public void ShouldClearUserNameAndPassword()
	{
		var connectionString = new NpgsqlConnectionStringBuilder{Host = "foo", Username = "user", Password = "pass"}.ToString();

		var result = new NpgsqlConnectionStringBuilder(connectionString.SetCredentials(true, null, null));

		result.Username.Should().Be.Null();
		result.Password.Should().Be.Null();
	}
}
