using System.Collections.Generic;
using System.Threading.Tasks;
using Schemes.Dal.Data;

namespace Schemes.Dal
{
    public class SchemesRepository : ISchemesRepository
    {
        public async Task<bool> Delete(int id) {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAll() {
            throw new System.NotImplementedException();
        }

        public async Task<Scheme> Get(int id) {
            throw new System.NotImplementedException();
        }

        public async Task<List<Scheme>> GetAll() {
            return new List<Scheme> {
                new Scheme() { Name = "Alma", Version=1 }
            };
        }

        public async Task<Scheme> Insert(Scheme scheme) {
            throw new System.NotImplementedException();
        }

        public async Task<Scheme> Update(int id, Scheme sheme) {
            throw new System.NotImplementedException();
        }
    }
}
