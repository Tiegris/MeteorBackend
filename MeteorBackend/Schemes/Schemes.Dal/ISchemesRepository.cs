using System.Collections.Generic;
using System.Threading.Tasks;
using Schemes.Domain.Requests;
using Schemes.Domain.Results;

namespace Schemes.Dal
{
    public interface ISchemesRepository
    {
        Task<SchemeResult> Insert(UpdateSchemeRequest scheme);
        Task<List<SchemeResult>> GetLatest(int limit);
        Task<bool> DeleteSingle(int id);
        Task<SchemeResult> Update(int id, UpdateSchemeRequest scheme);
        Task DeleteAll();
        Task<SchemeResult> GetSingle(int id);
    }
}