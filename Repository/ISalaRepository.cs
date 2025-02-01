using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineAPI.Repository
{
    public interface ISalaRepository
    {
        Task<List<Sala>> GetAllAsync();
        Task<Sala?> GetByIdAsync(int salaId);
    }
}
