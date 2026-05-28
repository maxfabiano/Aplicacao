using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository.Interfaces
{
    public interface IReadDatabase
    {
        Task<IEnumerable<T>> QueryAsync<T>(string queryText, object parameters = null, CommandType? commandType = null, CancellationToken ct = default);
        Task<T> QueryFirstOrDefaultAsync<T>(string queryText, object parameters = null, CommandType? commandType = null, CancellationToken ct = default);
    }
}
