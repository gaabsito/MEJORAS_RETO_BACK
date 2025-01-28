using Microsoft.AspNetCore.Mvc;
using CineAPI.Services;

namespace CineAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeliculasController : ControllerBase
    {
        private readonly IPeliculaService _peliculaService;
        private readonly ILogger<PeliculasController> _logger;

        public PeliculasController(IPeliculaService peliculaService, ILogger<PeliculasController> logger)
        {
            _peliculaService = peliculaService;
            _logger = logger;
        }

        // GET: api/peliculas
        [HttpGet]
        public async Task<ActionResult<List<Pelicula>>> GetPeliculas()
        {
            try
            {
                var peliculas = await _peliculaService.GetAllPeliculasAsync();
                return Ok(peliculas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener todas las películas");
                return StatusCode(500, "Error interno del servidor al obtener las películas");
            }
        }

        // GET: api/peliculas/5
        [HttpGet("{peliculaId}")]
        public async Task<ActionResult<Pelicula>> GetPeliculaById(int peliculaId)
        {
            try
            {
                var pelicula = await _peliculaService.GetPeliculaByIdAsync(peliculaId);
                return Ok(pelicula);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Película no encontrada: {PeliculaId}", peliculaId);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la película: {PeliculaId}", peliculaId);
                return StatusCode(500, "Error interno del servidor al obtener la película");
            }
        }

        // GET: api/peliculas/buscar-por-titulo?title=Matrix
        [HttpGet("buscar-por-titulo")]
        public async Task<ActionResult<List<Pelicula>>> GetPeliculasByTitle([FromQuery] string title)
        {
            try
            {
                var peliculas = await _peliculaService.GetPeliculasByTitleAsync(title);
                return Ok(peliculas);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Búsqueda por título inválida: {Title}", title);
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogInformation(ex, "No se encontraron películas con el título: {Title}", title);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar películas por título: {Title}", title);
                return StatusCode(500, "Error interno del servidor al buscar películas");
            }
        }

        // GET: api/peliculas/buscar-por-genero?genero=Acción
        [HttpGet("buscar-por-genero")]
        public async Task<ActionResult<List<Pelicula>>> GetPeliculasByGenero([FromQuery] string genero)
        {
            try
            {
                var peliculas = await _peliculaService.GetPeliculasByGeneroAsync(genero);
                return Ok(peliculas);
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Búsqueda por género inválida: {Genero}", genero);
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogInformation(ex, "No se encontraron películas del género: {Genero}", genero);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar películas por género: {Genero}", genero);
                return StatusCode(500, "Error interno del servidor al buscar películas");
            }
        }

        // POST: api/peliculas/inicializar
        [HttpPost("inicializar")]
        public async Task<IActionResult> InicializarDatos()
        {
            try
            {
                await _peliculaService.InicializarDatosAsync();
                return Ok("Datos inicializados correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al inicializar los datos");
                return StatusCode(500, "Error interno del servidor al inicializar los datos");
            }
        }
    }
}