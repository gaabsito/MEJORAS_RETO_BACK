using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CineAPI.Repository;

namespace CineAPI.Service
{
    public class SesionService : ISesionService
    {
        private readonly ISesionRepository _sesionRepository;

        public SesionService(ISesionRepository sesionRepository)
        {
            _sesionRepository = sesionRepository;
        }

        public async Task<List<Sesion>> GetAllSesionesAsync()
        {
            try
            {
                return await _sesionRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las sesiones", ex);
            }
        }

        public async Task<Sesion?> GetSesionByIdAsync(int sesionId)
        {
            try
            {
                var sesion = await _sesionRepository.GetByIdAsync(sesionId);
                if (sesion == null)
                {
                    throw new KeyNotFoundException($"No se encontró la sesión con ID {sesionId}");
                }
                return sesion;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la sesión con ID {sesionId}", ex);
            }
        }

        public async Task<List<Sesion>> GetSesionesBySalaIdAsync(int salaId)
        {
            try
            {
                var sesiones = await _sesionRepository.GetBySalaIdAsync(salaId);
                if (sesiones == null || sesiones.Count == 0)
                {
                    throw new KeyNotFoundException($"No se encontraron sesiones para la sala con ID {salaId}");
                }
                return sesiones;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener sesiones de la sala con ID {salaId}", ex);
            }
        }

        public async Task<int> AddSesionAsync(Sesion sesion)
        {
            try
            {
                return await _sesionRepository.AddAsync(sesion);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar una nueva sesión", ex);
            }
        }

        public async Task<bool> DeleteSesionAsync(int sesionId)
        {
            try
            {
                return await _sesionRepository.DeleteAsync(sesionId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al eliminar la sesión con ID {sesionId}", ex);
            }
        }
    }
}
