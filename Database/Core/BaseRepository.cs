using Dapper;
using Database.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace Database.Repository
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly Func<IDbConnection> _connectionFactory;

        protected BaseRepository(Func<IDbConnection> connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        // Wrapper centralizado para gerenciar conexões
        protected async Task<T> WithConnectionAsync<T>(Func<IDbConnection, Task<T>> func)
        {
            using var connection = _connectionFactory();
            // A conexão é aberta apenas quando necessária
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return await func(connection);
        }

        public async Task<int> ExecuteAsync(string sql, object parameters = null, CancellationToken ct = default)
        {
            return await WithConnectionAsync(async conn =>
                await conn.ExecuteAsync(new CommandDefinition(sql, parameters, cancellationToken: ct)));
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object parameters = null, CancellationToken ct = default)
        {
            return await WithConnectionAsync(async conn =>
                await conn.QueryAsync<T>(new CommandDefinition(sql, parameters, cancellationToken: ct)));
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object parameters = null, CancellationToken ct = default)
        {
            return await WithConnectionAsync(async conn =>
                await conn.QueryFirstOrDefaultAsync<T>(new CommandDefinition(sql, parameters, cancellationToken: ct)));
        }

        // ALERTA: Tabela dinâmica pode ser perigosa. Valide o input do usuário antes de chamar este método.
        public async Task<IEnumerable<T>> GetAllAsync<T>(string tableName, CancellationToken ct = default)
        {
            // Validação simples de segurança contra SQL Injection em nomes de tabelas
            if (!System.Text.RegularExpressions.Regex.IsMatch(tableName, @"^[a-zA-Z0-9_]+$"))
                throw new ArgumentException("Nome de tabela inválido");

            return await QueryAsync<T>($"SELECT * FROM {tableName}", ct: ct);
        }
    }
}