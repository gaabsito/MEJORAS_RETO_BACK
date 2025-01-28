using CineAPI.Repository;

namespace CineAPI.Service
{
    public class PeliculaService : IPeliculaService
    {
        private readonly IPeliculaRepository _peliculaRepository;

        public PeliculaService(IPeliculaRepository peliculaRepository)
        {
            _peliculaRepository = peliculaRepository;
        }

        public async Task<List<Pelicula>> GetAllPeliculasAsync()
        {
            try
            {
                return await _peliculaRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener todas las películas", ex);
            }
        }

        public async Task<Pelicula?> GetPeliculaByIdAsync(int peliculaId)
        {
            try
            {
                var pelicula = await _peliculaRepository.GetByIdAsync(peliculaId);
                if (pelicula == null)
                {
                    throw new KeyNotFoundException($"No se encontró la película con ID {peliculaId}");
                }
                return pelicula;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener la película con ID {peliculaId}", ex);
            }
        }

        public async Task<List<Pelicula>> GetPeliculasByTitleAsync(string title)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(title))
                {
                    throw new ArgumentException("El título de búsqueda no puede estar vacío");
                }

                var peliculas = await _peliculaRepository.GetByTitleAsync(title);
                if (!peliculas.Any())
                {
                    throw new KeyNotFoundException($"No se encontraron películas con el título '{title}'");
                }
                return peliculas;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar películas con el título '{title}'", ex);
            }
        }

        public async Task<List<Pelicula>> GetPeliculasByGeneroAsync(string genero)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(genero))
                {
                    throw new ArgumentException("El género de búsqueda no puede estar vacío");
                }

                var peliculas = await _peliculaRepository.GetByGeneroAsync(genero);
                if (!peliculas.Any())
                {
                    throw new KeyNotFoundException($"No se encontraron películas del género '{genero}'");
                }
                return peliculas;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar películas del género '{genero}'", ex);
            }
        }

        public async Task InicializarDatosAsync()
        {
            try
            {
                await _peliculaRepository.InicializarDatosAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al inicializar los datos de películas", ex);
            }
        }
    }
}