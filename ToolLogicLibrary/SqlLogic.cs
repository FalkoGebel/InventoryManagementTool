using Dapper;
using Microsoft.Data.SqlClient;

namespace ToolLogicLibrary
{
    public class SqlLogic
    {
        public static void CreateDatabase(string server, string database)
        {
            if (GetDatabasesFromServer(server).Contains(database))
                throw new ArgumentException(Properties.Resources.EXP_DATABASE_EXIST.Replace("[DATABASENAME]", database));

            using SqlConnection cnnCreate = GetOpenConnection(server);
            {
                cnnCreate.Execute($"CREATE DATABASE {database}");
            }

            using SqlConnection cnnAddTables = GetOpenConnection(server, database);
            {
                // Table "Area"
                cnnAddTables.Query("CREATE TABLE [dbo].[Area] (\r\n\t[Code] [varchar](20) NOT NULL,\r\n\t[Description] [varchar](250) NOT NULL,\r\nCONSTRAINT[PK_Area] PRIMARY KEY CLUSTERED\r\n(\r\n\t[Code] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY]");

                // Table "Product"
                cnnAddTables.Query("CREATE TABLE [dbo].[Product] (\r\n\t[Code] [varchar](20) NOT NULL,\r\n\t[Description] [varchar](250) NOT NULL,\r\nCONSTRAINT[PK_Product] PRIMARY KEY CLUSTERED\r\n(\r\n\t[Code] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY]");

                // Table "Adjustment"
                cnnAddTables.Query("CREATE TABLE [dbo].[Adjustment] (\r\n\t[Id] [int] NOT NULL,\r\n\t[Area Code] [varchar](20) NOT NULL,\r\n\t[Product Code] [varchar](20) NOT NULL,\r\n\t[Posting Date] [date] NOT NULL,\r\n\t[Quantity] [int] NOT NULL,\r\nCONSTRAINT[PK_Adjustment] PRIMARY KEY CLUSTERED\r\n(\r\n\t[Id] ASC\r\n)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]\r\n) ON [PRIMARY]");
                cnnAddTables.Query("ALTER TABLE [dbo].[Adjustment] WITH CHECK ADD CONSTRAINT [FK_Adjustment_Area] FOREIGN KEY([Area Code])\r\nREFERENCES [dbo].[Area] ([Code])");
                cnnAddTables.Query("ALTER TABLE [dbo].[Adjustment] WITH CHECK ADD CONSTRAINT [FK_Adjustment_Product] FOREIGN KEY([Product Code])\r\nREFERENCES [dbo].[Product] ([Code])");
            }
        }

        public static List<string> GetDatabasesFromServer(string server)
        {
            List<string> output;

            using (SqlConnection cnn = GetOpenConnection(server))
            {
                output = cnn.Query<string>("SELECT [name] FROM sys.databases;").AsList();
            }

            return output;
        }

        public static List<string> GetTablesFromDatabase(string server, string database)
        {
            List<string> output = [];

            using (SqlConnection cnn = GetOpenConnection(server))
            {
                output = cnn.Query<string>($"SELECT [TABLE_NAME] FROM[{database}].INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE';").AsList();
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
