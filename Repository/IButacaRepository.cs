using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineAPI.Repository
{
    public interface IButacaRepository
    {
        Task<List<Butaca>> GetAllAsync();
        Task<Butaca?> GetByIdAsync(int butacaId);
        Task<List<Butaca>> GetBySalaAsync(int salaId);
        Task<int> AddAsync(Butaca butaca);
        Task<bool> UpdateEstadoAsync(int butacaId, string estado);
        Task<bool> DeleteAsync(int butacaId);
    }
}
