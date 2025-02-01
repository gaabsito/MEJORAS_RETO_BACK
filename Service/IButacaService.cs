using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineAPI.Service
{
    public interface IButacaService
    {
        Task<List<Butaca>> GetAllButacasAsync();
        Task<Butaca?> GetButacaByIdAsync(int butacaId);
        Task<List<Butaca>> GetButacasBySalaAsync(int salaId);
        Task<int> AddButacaAsync(Butaca butaca);
        Task<bool> UpdateEstadoAsync(int butacaId, string estado);
        Task<bool> DeleteButacaAsync(int butacaId);
    }
}