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
    public class SqlReadDatabase : IReadDatabase
    {
        private readonly Func<IDbConnection> _connectionFactory;

        public SqlReadDatabase(Func<IDbConnection> connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        private async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            using var connection = _connectionFactory();
            if (connection.State != ConnectionState.Open) connection.Open();
            return await func(connection);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string queryText, object parameters = null, CommandType? commandType = null, CancellationToken ct = default)
        {
            return await WithConnectionAsync(async conn =>
                await conn.QueryAsync<T>(new CommandDefinition(
                    queryText,
                    parameters,
                    commandType: commandType, // Suporte a Stored Procedures e Views
                    cancellationToken: ct)));
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string queryText, object parameters = null, CommandType? commandType = null, CancellationToken ct = default)
        {
            return await WithConnectionAsync(async conn =>
                await conn.QueryFirstOrDefaultAsync<T>(new CommandDefinition(
                    queryText,
                    parameters,
                    commandType: commandType,
                    cancellationToken: ct)));
        }
    }
}
