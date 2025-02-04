using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineAPI.Repository
{
    public interface ICineRepository
    {
        Task<List<Cine>> GetAllAsync();
        Task<Cine?> GetByIdAsync(int cineId);
        Task<List<Sala>> GetSalasByCineIdAsync(int cineId);
    }
}
