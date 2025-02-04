using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CineAPI.Repository;

namespace CineAPI.Service
{
    public class CineService : ICineService
    {
        private readonly ICineRepository _cineRepository;

        public CineService(ICineRepository cineRepository)
        {
            _cineRepository = cineRepository;
        }

        public async Task<List<Cine>> GetAllCinesAsync()
        {
            try
            {
                return await _cineRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todos los cines", ex);
            }
        }

        public async Task<Cine?> GetCineByIdAsync(int cineId)
        {
            try
            {
                var cine = await _cineRepository.GetByIdAsync(cineId);
                if (cine == null)
                {
                    throw new KeyNotFoundException($"No se encontró el cine con ID {cineId}");
                }
                return cine;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el cine con ID {cineId}", ex);
            }
        }

        public async Task<List<Sala>> GetSalasByCineIdAsync(int cineId)
        {
            try
            {
                var salas = await _cineRepository.GetSalasByCineIdAsync(cineId);
                if (!salas.Any())
                {
                    throw new KeyNotFoundException($"No se encontraron salas para el cine con ID {cineId}");
                }
                return salas;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener las salas del cine con ID {cineId}", ex);
            }
        }
    }
}
