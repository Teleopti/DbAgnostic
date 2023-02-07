using System.Data.SqlClient;
using Npgsql;

namespace DbAgnostic;

public static class ConnectionStringMutationExtensions
{
    public static string ChangeDatabase(this string connectionString, string newDatabase)
    {
        return new ConnectionStringDbSelector(connectionString).PickFunc(
            () =>
            {
                var x = new SqlConnectionStringBuilder(connectionString);
                if (newDatabase == null)
                    x.Remove("Initial Catalog");
                else
                    x.InitialCatalog = newDatabase;
                return x.ToString();
            },
            () => new NpgsqlConnectionStringBuilder(connectionString) {Database = newDatabase}.ToString());
    }

    public static string RemoveDatabase(this string connectionString) =>
        connectionString.ChangeDatabase(null);

    public static string PointToMasterDatabase(this string connectionString)
    {
        return new ConnectionStringDbSelector(connectionString).PickFunc(
            () => new SqlConnectionStringBuilder(connectionString) {InitialCatalog = "master"}.ToString(),
            () => new NpgsqlConnectionStringBuilder(connectionString) {Database = "postgres"}.ToString());
    }

    public static string ChangeServer(this string connectionString, string server)
    {
        return new ConnectionStringDbSelector(connectionString).PickFunc(
            () =>
            {
                var x = new SqlConnectionStringBuilder(connectionString);
                if (server == null)
                    x.Remove("Data Source");
                else
                    x.DataSource = server;
                return x.ToString();
            },
            () => new NpgsqlConnectionStringBuilder(connectionString) {Host = server}.ToString());
    }

    public static string RemoveServer(this string connectionString) =>
        connectionString.ChangeServer(null);

    public static string ChangeApplicationName(this string connectionString, string applicationName)
    {
        return new ConnectionStringDbSelector(connectionString).PickFunc(
            () =>
            {
                var x = new SqlConnectionStringBuilder(connectionString);
                if (applicationName == null)
                    x.Remove("Application Name");
                else
                    x.ApplicationName = applicationName;
                return x.ToString();
            },
            () => new NpgsqlConnectionStringBuilder(connectionString) {ApplicationName = applicationName}.ToString());
    }

    public static string RemoveApplicationName(this string connectionString) =>
        connectionString.ChangeApplicationName(null);

    public static string SetUserNameAndPassword(this string connectionString, string userName, string password) =>
        new ConnectionStringDbSelector(connectionString).PickFunc(
            () =>
            {
                var x = new SqlConnectionStringBuilder(connectionString);
                if (userName == null)
                    x.Remove("User ID");
                else
                    x.UserID = userName;
                if (password == null)
                    x.Remove("Password");
                else
                    x.Password = password;
                x.Remove("Integrated security");
                return x.ToString();
            },
            () =>
            {
                var x = new NpgsqlConnectionStringBuilder(connectionString) {Username = userName, Password = password};
                x.Remove("Integrated security");
                return x.ToString();
            }
        );

    public static string SetCredentials(this string connectionString, bool useIntegratedSecurity, string userName, string password) =>
        new ConnectionStringDbSelector(connectionString).PickFunc(
            () =>
            {
                if (useIntegratedSecurity)
                {
                    var x = new SqlConnectionStringBuilder(connectionString) {IntegratedSecurity = true};
                    x.Remove("User Id");
                    x.Remove("Password");
                    return x.ToString();
                }

                return SetUserNameAndPassword(connectionString, userName, password);
            },
            () =>
            {
                if (useIntegratedSecurity)
                    return new NpgsqlConnectionStringBuilder(connectionString) {IntegratedSecurity = true, Username = null, Password = null}.ToString();
                return SetUserNameAndPassword(connectionString, userName, password);
            });

    public static string SetCredentials(this string connectionString, string sourceConnectionString) =>
        connectionString.SetCredentials(
            sourceConnectionString.IntegratedSecurity(),
            sourceConnectionString.UserName(),
            sourceConnectionString.Password()
        );
}