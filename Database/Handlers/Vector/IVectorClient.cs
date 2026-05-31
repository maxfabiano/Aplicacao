namespace Database.Handlers.Vector
{
    public interface IVectorClient
    {
        Task<int> DeleteAsync(IEnumerable<string> idsParaDeletar, CancellationToken ct);
        object GerarEmbedding(string queryText);
        Task<IEnumerable<T>> PesquisarPorVetor<T>(object vetorDeBusca, object parameters, CancellationToken ct);
        Task<int> UpsertAsync(IEnumerable<VectorData> vetoresParaSalvar, CancellationToken ct);
    }
}