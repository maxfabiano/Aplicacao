using Database.Core.Interfaces;
using Database.Handlers.Interfaces;
using Database.Handlers.Sql;
using Database.Infrastructure.Connections;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Database.Infrastructure.IoC
{
    public static class DatabaseServiceRegistration
    {
        public static IServiceCollection AddDatabaseInfrastructure(
            this IServiceCollection services,
            string databaseProvider,
            string connectionString)
        {
            /*
            // 1. Registramos a Fábrica correta com base na configuração do AppSettings
            switch (databaseProvider.ToUpper())
            {
                case "SQLSERVER":
                    services.AddSingleton<IDbConnectionFactory>(new SqlServerConnectionFactory(connectionString));
                    break;
                case "POSTGRES":
                    services.AddSingleton<IDbConnectionFactory>(new PostgresConnectionFactory(connectionString));
                    break;
                case "MYSQL":
                    services.AddSingleton<IDbConnectionFactory>(new MySqlConnectionFactory(connectionString));
                    break;
                default:
                    throw new ArgumentException($"Provedor de banco de dados {databaseProvider} não suportado.");
            }
            */
            // 2. Registramos as classes do CQRS (Elas vão receber a fábrica definida acima automaticamente!)
            services.AddScoped<IReadDatabase, SqlReadDatabase>();
            services.AddScoped<IWriteDatabase, SqlWriteDatabase>();

            return services;
        }
    }
}