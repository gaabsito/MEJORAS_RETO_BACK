using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/cines")]
public class CinesController : ControllerBase
{
    // Obtener lista de cines con sus salas y sesiones
    [HttpGet]
    public ActionResult<IEnumerable<Cine>> GetCines()
    {
        return Ok(DatosCines.Cines);
    }

    // Obtener un cine por ID con sus salas y sesiones
    [HttpGet("cineId/{cineId}")]
    public ActionResult<Cine> GetCineById(int cineId)
    {
        var cine = DatosCines.Cines.FirstOrDefault(c => c.CineId == cineId);
        if (cine == null)
        {
            return NotFound($"Cine con ID {cineId} no encontrado.");
        }
        return Ok(cine);
    }

    // Obtener las salas de un cine por ID
    [HttpGet("cineId/{cineId}/salas")]
    public ActionResult<IEnumerable<Sala>> GetSalasPorCineId(int cineId)
    {
        var cine = DatosCines.Cines.FirstOrDefault(c => c.CineId == cineId);
        if (cine == null)
        {
            return NotFound($"Cine con ID {cineId} no encontrado.");
        }

        return Ok(cine.Salas);
    }

    // Obtener la sala por ID
    [HttpGet("cineId/{cineId}/salas/{salaId}")]
    public ActionResult<Sala> GetSalaPorId(int cineId, int salaId)
    {
        var cine = DatosCines.Cines.FirstOrDefault(c => c.CineId == cineId);
        if (cine == null)
        {
            return NotFound($"Cine con ID {cineId} no encontrado.");
        }

        var sala = cine.Salas.FirstOrDefault(s => s.SalaId == salaId);
        if (sala == null)
        {
            return NotFound($"Sala con ID {salaId} no encontrada en el cine con ID {cineId}.");
        }

        return Ok(sala);
    }

    // Obtener sesiones de una sala específica en un cine
    [HttpGet("cineId/{cineId}/salas/{salaId}/sesiones")]
    public ActionResult<IEnumerable<Sesion>> GetSesionesPorSalaId(int cineId, int salaId)
    {
        var cine = DatosCines.Cines.FirstOrDefault(c => c.CineId == cineId);
        if (cine == null)
        {
            return NotFound($"Cine con ID {cineId} no encontrado.");
        }

        var sala = cine.Salas.FirstOrDefault(s => s.SalaId == salaId);
        if (sala == null)
        {
            return NotFound($"Sala con ID {salaId} no encontrada en el cine con ID {cineId}.");
        }

        return Ok(sala.Sesiones);
    }

    // Obtener una sesion específica por cine, sala y sesion
    [HttpGet("cineId/{cineId}/salas/{salaId}/sesiones/{sesionId}")]
    public ActionResult<Sesion> GetSesionById(int cineId, int salaId, int sesionId)
    {
        var cine = DatosCines.Cines.FirstOrDefault(c => c.CineId == cineId);
        if (cine == null)
        {
            return NotFound($"Cine con ID {cineId} no encontrado.");
        }

        var sala = cine.Salas.FirstOrDefault(s => s.SalaId == salaId);
        if (sala == null)
        {
            return NotFound($"Sala con ID {salaId} no encontrada en el cine con ID {cineId}.");
        }

        var sesion = sala.Sesiones.FirstOrDefault(f => f.SesionId == sesionId);

        if (sesion == null)
        {
            return NotFound($"Sesion con ID {sesionId} no encontrada en la sala con ID {salaId} del cine con ID {cineId}.");
        }

        return Ok(sesion);
    }
}