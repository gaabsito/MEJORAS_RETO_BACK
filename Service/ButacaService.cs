using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CineAPI.Repository;

namespace CineAPI.Service
{
    public class ButacaService : IButacaService
    {
        private readonly IButacaRepository _butacaRepository;

        public ButacaService(IButacaRepository butacaRepository)
        {
            _butacaRepository = butacaRepository;
        }

        public async Task<List<Butaca>> GetAllButacasAsync()
        {
            try
            {
                return await _butacaRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las butacas", ex);
            }
        }

        public async Task<Butaca?> GetButacaByIdAsync(int butacaId)
        {
            try
            {
                var butaca = await _butacaRepository.GetByIdAsync(butacaId);
                if (butaca == null)
                {
                    throw new KeyNotFoundException($"No se encontró la butaca con ID {butacaId}");
                }
                return butaca;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la butaca con ID {butacaId}", ex);
            }
        }

        public async Task<List<Butaca>> GetButacasBySalaAsync(int salaId)
        {
            try
            {
                var butacas = await _butacaRepository.GetBySalaAsync(salaId);
                if (!butacas.Any())
                {
                    throw new KeyNotFoundException($"No se encontraron butacas para la sala con ID {salaId}");
                }
                return butacas;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las butacas de la sala con ID {salaId}", ex);
            }
        }

        public async Task<int> AddButacaAsync(Butaca butaca)
        {
            try
            {
                return await _butacaRepository.AddAsync(butaca);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar una nueva butaca", ex);
            }
        }

        public async Task<bool> UpdateEstadoAsync(int butacaId, string estado)
        {
            try
            {
                return await _butacaRepository.UpdateEstadoAsync(butacaId, estado);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el estado de la butaca con ID {butacaId}", ex);
            }
        }

        public async Task<bool> DeleteButacaAsync(int butacaId)
        {
            try
            {
                return await _butacaRepository.DeleteAsync(butacaId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la butaca con ID {butacaId}", ex);
            }
        }
    }
}