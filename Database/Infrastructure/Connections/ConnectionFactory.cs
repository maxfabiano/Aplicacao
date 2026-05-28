using Database.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Infrastructure.Connections
{
    public class ConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        /*
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
        */
    }
}
