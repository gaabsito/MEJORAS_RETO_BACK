using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/sesiones")]
public class SesionesController : ControllerBase
{
    // Obtener todas las sesiones disponibles en todos los cines y salas
    [HttpGet]
    public ActionResult<IEnumerable<Sesion>> GetSesiones()
    {
        // Recopila todas las sesiones desde todas las salas y cines
        var sesiones = DatosCines.Cines
            .SelectMany(c => c.Salas) // Itera sobre todas las salas de todos los cines
            .SelectMany(s => s.Sesiones) // Itera sobre todas las sesiones de cada sala
            .ToList(); // Convierte el resultado en una lista

        return Ok(sesiones); // Devuelve las sesiones en formato JSON
    }

    // Obtener una sesión específica utilizando su ID
    [HttpGet("{sesionId}")]
    public ActionResult<Sesion> GetSesionById(int sesionId)
    {
        // Busca una sesión específica por su ID en todas las salas y cines
        var sesion = DatosCines.Cines
            .SelectMany(c => c.Salas) // Itera sobre todas las salas
            .SelectMany(s => s.Sesiones) // Itera sobre todas las sesiones
            .FirstOrDefault(f => f.SesionId == sesionId); // Encuentra la sesión que coincide con el ID

        if (sesion == null)
        {
            // Si no se encuentra la sesión, devuelve un error 404
            return NotFound($"Sesión con ID {sesionId} no encontrada.");
        }

        return Ok(sesion); // Devuelve la sesión encontrada en formato JSON
    }

    // Obtener las butacas asociadas a una sesión específica
    [HttpGet("{sesionId}/butacas")]
    public ActionResult<IEnumerable<Butaca>> GetButacasBySesionId(int sesionId)
    {
        // Busca la sesión por ID
        var sesion = DatosCines.Cines
            .SelectMany(c => c.Salas)
            .SelectMany(s => s.Sesiones)
            .FirstOrDefault(f => f.SesionId == sesionId);

        if (sesion == null)
        {
            // Si no se encuentra la sesión, devuelve un error 404
            return NotFound($"Sesión con ID {sesionId} no encontrada.");
        }

        return Ok(sesion.Butacas); // Devuelve las butacas asociadas a la sesión
    }

    // Obtener todas las sesiones disponibles en una sala específica
    [HttpGet("sala/{salaId}")]
    public ActionResult<IEnumerable<Sesion>> GetSesionesBySalaId(int salaId)
    {
        // Busca la sala específica por su ID
        var sala = DatosCines.Cines
            .SelectMany(c => c.Salas) // Itera sobre todas las salas
            .FirstOrDefault(s => s.SalaId == salaId); // Encuentra la sala que coincide con el ID

        if (sala == null)
        {
            // Si no se encuentra la sala, devuelve un error 404
            return NotFound($"Sala con ID {salaId} no encontrada.");
        }

        // Si la sala no tiene sesiones, genera sesiones por defecto
        if (sala.Sesiones == null || !sala.Sesiones.Any())
        {
            sala.Sesiones = GenerarSesionesPorDefecto(salaId);
        }

        return Ok(sala.Sesiones); // Devuelve las sesiones de la sala en formato JSON
    }

    // Obtener todas las sesiones para una película específica en un cine específico
    [HttpGet("cine/{cineId}/pelicula/{peliculaId}")]
    public ActionResult<IEnumerable<Sesion>> GetSesionesByCineYPelicula(int cineId, int peliculaId)
    {
        // Busca las sesiones asociadas a un cine y película específicos
        var sesiones = DatosCines.Cines
            .Where(c => c.CineId == cineId) // Filtra por cine
            .SelectMany(c => c.Salas) // Itera sobre las salas del cine
            .SelectMany(s => s.Sesiones) // Itera sobre las sesiones de cada sala
            .Where(f => f.PeliculaId == peliculaId) // Filtra las sesiones por el ID de la película
            .ToList(); // Convierte el resultado en una lista

        if (!sesiones.Any())
        {
            // Si no hay sesiones, devuelve un error 404
            return NotFound($"No se encontraron sesiones para la película con ID {peliculaId} en el cine con ID {cineId}.");
        }

        return Ok(sesiones); // Devuelve las sesiones encontradas
    }

    // Método auxiliar: Genera sesiones por defecto si no existen
    private List<Sesion> GenerarSesionesPorDefecto(int salaId)
    {
        // Genera una lista de sesiones por defecto
        return new List<Sesion>
        {
            new Sesion
            {
                SesionId = salaId * 100 + 1, // Genera un ID único basado en la sala
                SalaId = salaId,
                PeliculaId = 101, // ID de película por defecto
                Butacas = GenerarButacasPorDefecto(salaId) // Genera butacas por defecto
            },
            new Sesion
            {
                SesionId = salaId * 100 + 2, // Genera otro ID único
                SalaId = salaId,
                PeliculaId = 102,
                Butacas = GenerarButacasPorDefecto(salaId)
            }
        };
    }

    // Método auxiliar: Genera una lista de butacas por defecto
    private List<Butaca> GenerarButacasPorDefecto(int salaId)
    {
        return Enumerable.Range(1, 150).Select(id => new Butaca
        {
            ButacaId = id,
            SalaId = salaId,
            Estado = "Disponible",
            PrecioButaca = 6.90
        }).ToList();
    }
}

// Notas generales:
// - Este controlador proporciona varios endpoints para interactuar con las sesiones de los cines.
// - Está diseñado para manejar errores como sesiones o salas inexistentes.
// - Incluye métodos auxiliares para generar datos por defecto (sesiones y butacas).
// - Los métodos devuelven resultados en formato JSON para ser consumidos por aplicaciones frontend.
