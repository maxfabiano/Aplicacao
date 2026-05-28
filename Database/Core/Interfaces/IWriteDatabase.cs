using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repository.Interfaces
{
    public interface IWriteDatabase
    {
        Task<int> ExecuteAsync(string commandText, object parameters = null, CommandType? commandType = null, CancellationToken ct = default);
    }
}
