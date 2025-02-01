using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineAPI.Service
{
    public interface ISalaService
    {
        Task<List<Sala>> GetAllSalasAsync();
        Task<Sala?> GetSalaByIdAsync(int salaId);
    }
}