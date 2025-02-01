using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineAPI.Service
{
    public interface ISesionService
    {
        Task<List<Sesion>> GetAllSesionesAsync();
        Task<Sesion?> GetSesionByIdAsync(int sesionId);
        Task<List<Sesion>> GetSesionesBySalaIdAsync(int salaId);
        Task<int> AddSesionAsync(Sesion sesion);
        Task<bool> DeleteSesionAsync(int sesionId);
    }
}