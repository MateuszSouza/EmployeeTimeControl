using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Repository.Repositories
{
    public class DatabaseConnection
    {
        private readonly IConfiguration _configuration;
        protected NpgsqlConnection Connection;

        public DatabaseConnection(IConfiguration configuration)
        {
            _configuration = configuration;
            var connectionString = _configuration.GetSection("connection").Value;
            Connection = new NpgsqlConnection(connectionString);
        }

        public NpgsqlConnection GetOpendConnections()
        {
            if (Connection.State == ConnectionState.Open)
                return Connection;

            Connection.Open();
            return Connection;
        }

        public void CloseConnection()
        {
            if (Connection.State == ConnectionState.Closed)
                return;
            Connection.Close();
        }
    }
}