using Microsoft.Data.SqlClient;

namespace ADO.NET_SAMPLE.Database
{
    public class DbConnectionFactory
    {
        private readonly  string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);

        }


    }
}
