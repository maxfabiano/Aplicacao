using Database.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace Database.Infrastructure.Sql
{
    public class SqlWriteDatabase : IWriteDatabase
    {
        private readonly Func<IDbConnection> _connectionFactory;

        public SqlWriteDatabase(Func<IDbConnection> connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        private async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            using var connection = _connectionFactory();
            if (connection.State != ConnectionState.Open) connection.Open();
            return await func(connection);
        }

        public async Task<int> ExecuteAsync(string commandText, object parameters = null, CommandType? commandType = null, CancellationToken ct = default)
        {
            return await WithConnectionAsync(async conn =>
                await conn.ExecuteAsync(new CommandDefinition(
                    commandText,
                    parameters,
                    commandType: commandType, // Essencial para rodar Procedures de Insert/Update
                    cancellationToken: ct)));
        }
    }
}
