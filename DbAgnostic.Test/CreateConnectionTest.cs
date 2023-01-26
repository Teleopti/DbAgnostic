using Microsoft.Data.SqlClient;
using Npgsql;
using NUnit.Framework;
using SharpTestsEx;

namespace DbAgnostic.Test;

public class CreateConnectionTest
{
	[Test]
	public void ShouldCreateSqlConnection()
	{
		var connectionString = new SqlConnectionStringBuilder {DataSource = "foo"}.ToString();

		var result = connectionString.CreateConnection();

		result.Should().Be.OfType<SqlConnection>();
	}

	[Test]
	public void ShouldCreateNpgsqlConnection()
	{
		var connectionString = new NpgsqlConnectionStringBuilder {Host = "foo"}.ToString();

		var result = connectionString.CreateConnection();

		result.Should().Be.OfType<NpgsqlConnection>();
	}
}
