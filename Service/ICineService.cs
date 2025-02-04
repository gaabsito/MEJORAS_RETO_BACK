using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineAPI.Service
{
    public interface ICineService
    {
        Task<List<Cine>> GetAllCinesAsync();
        Task<Cine?> GetCineByIdAsync(int cineId);
        Task<List<Sala>> GetSalasByCineIdAsync(int cineId);
    }
}
