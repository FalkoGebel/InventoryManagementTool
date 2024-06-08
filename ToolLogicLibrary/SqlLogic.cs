using Dapper;
using Microsoft.Data.SqlClient;

namespace ToolLogicLibrary
{
    public class SqlLogic
    {
        public static List<string> GetDatabasesFromServer(string server)
        {
            List<string> output;

            using (SqlConnection cnn = GetOpenConnection(server))
            {
                output = cnn.Query<string>("SELECT [name] FROM sys.databases;").AsList();
            }

            return output;
        }
        private static SqlConnection GetOpenConnection(string server, string? database = null)
        {
            string connectionString = $@"Data Source={server};{(database != null ? $"Initial Catalog={database};" : "")}Integrated Security=SSPI;TrustServerCertificate=true;";

            SqlConnection cnn = new(connectionString);
            cnn.Open();

            return cnn;
        }
    }
}
