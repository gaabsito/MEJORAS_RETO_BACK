using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CineAPI.Repository;

namespace CineAPI.Service
{
    public class SalaService : ISalaService
    {
        private readonly ISalaRepository _salaRepository;

        public SalaService(ISalaRepository salaRepository)
        {
            _salaRepository = salaRepository;
        }

        public async Task<List<Sala>> GetAllSalasAsync()
        {
            try
            {
                return await _salaRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las salas", ex);
            }
        }

        public async Task<Sala?> GetSalaByIdAsync(int salaId)
        {
            try
            {
                var sala = await _salaRepository.GetByIdAsync(salaId);
                if (sala == null)
                {
                    throw new KeyNotFoundException($"No se encontró la sala con ID {salaId}");
                }
                return sala;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la sala con ID {salaId}", ex);
            }
        }
    }
}
