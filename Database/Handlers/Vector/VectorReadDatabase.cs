using Database.Handlers.Interfaces;
using System.Data;

namespace Database.Handlers.Vector
{
    // Bancos vetoriais não usam SQL, eles usam arrays (embeddings) ou JSONs
    public class VectorReadDatabase : IReadDatabase
    {
        // Ao invés de IDbConnection, usa o cliente do banco vetorial
        private readonly IVectorClient _vectorClient;

        public async Task<IEnumerable<T>> QueryAsync<T>(string queryText, object parameters = null, CommandType? commandType = null, CancellationToken ct = default)
        {
            // queryText aqui pode ser tratado como a string a ser convertida em Embedding (Vetor)
            var vetorDeBusca = _vectorClient.GerarEmbedding(queryText);

            // parameters pode conter metadados de filtro
            return await _vectorClient.PesquisarPorVetor<T>(vetorDeBusca, parameters, ct);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string queryText, object parameters = null, CommandType? commandType = null, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        // ...
    }
}