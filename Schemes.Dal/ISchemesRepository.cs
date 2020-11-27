using System.Collections.Generic;
using System.Threading.Tasks;
using Schemes.Dal.Data;

namespace Schemes.Dal
{
    public interface ISchemesRepository
    {
        Task<Scheme> Insert(Scheme scheme);
        Task<List<Scheme>> GetAll();
        Task<bool> Delete(int id);
        Task<Scheme> Update(int id, Scheme scheme);
        Task DeleteAll();
        Task<Scheme> Get(int id);
    }
}