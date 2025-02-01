using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineAPI.Repository
{
public interface ISesionRepository
{
    Task<List<Sesion>> GetAllAsync();
    Task<Sesion?> GetByIdAsync(int sesionId);
    Task<List<Sesion>> GetBySalaIdAsync(int salaId);
    Task<int> AddAsync(Sesion sesion);
    Task<bool> DeleteAsync(int sesionId);
}

}
