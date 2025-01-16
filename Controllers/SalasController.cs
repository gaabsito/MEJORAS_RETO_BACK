using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/salas")]
public class SalasController : ControllerBase
{
    // Obtener todas las salas
    [HttpGet]
    public ActionResult<IEnumerable<Sala>> GetAllSalas()
    {
        var salas = DatosCines.Cines
            .SelectMany(c => c.Salas)
            .ToList();
            
    if (!salas.Any())
        {
            return NotFound("No se encontraron salas.");
        }

        return Ok(salas);
    }

    // Obtener una sala específica por ID
    [HttpGet("{salaId}")]
    public ActionResult<IEnumerable<Sala>> GetBySalaId(int salaId)
    {
        var sala = DatosCines.Cines
            .SelectMany(c => c.Salas)
            .FirstOrDefault(s => s.SalaId == salaId);

        if (sala == null)
        {
            return NotFound($"Sala con ID {salaId} no encontrada.");
        }

        return Ok(sala);
    }
}