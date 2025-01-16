using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tickets")]
public class TicketsController : ControllerBase
{
    private static List<Ticket> Tickets = new List<Ticket>();

    // Crear un ticket para una sesión seleccionada
    [HttpPost("crear-tickets")]
    public ActionResult<IEnumerable<Ticket>> CreateMultipleTickets([FromBody] List<Ticket> tickets)
    {
        var createdTickets = new List<Ticket>();

        foreach (var ticket in tickets)
        {
            // Validar si existe la sesión
            var sesion = DatosCines.Cines
                .SelectMany(c => c.Salas)
                .SelectMany(s => s.Sesiones)
                .FirstOrDefault(f => f.SesionId == ticket.SesionId);

            if (sesion == null)
            {
                return BadRequest($"La sesión con ID {ticket.SesionId} no existe.");
            }

            // Validar si existe la butaca
            var butaca = sesion.Butacas.FirstOrDefault(a => a.ButacaId == ticket.ButacaId);
            if (butaca == null || butaca.Estado != "Disponible")
            {
                return BadRequest($"La butaca con ID {ticket.ButacaId} no está disponible.");
            }

            // Crear el ticket
            ticket.FechaDeCompra = DateTime.Now;
            ticket.TicketId = Tickets.Count > 0 ? Tickets.Max(t => t.TicketId) + 1 : 1;

            // Actualizar el estado de la butaca
            butaca.Estado = "Reservado";
            butaca.TicketId = ticket.TicketId;

            // Añadir el ticket
            Tickets.Add(ticket);
            createdTickets.Add(ticket);
        }

        return CreatedAtAction(nameof(GetTicketById), new { ticketId = createdTickets.First().TicketId }, createdTickets);
    }

    // Obtener todos los tickets
    [HttpGet]
    public ActionResult<IEnumerable<Ticket>> GetAllTickets()
    {
        return Ok(Tickets);
    }

    // Obtener un ticket específico por su ID
    [HttpGet("ticketId/{ticketId}")]
    public ActionResult<Ticket> GetTicketById(int ticketId)
    {
        var ticket = Tickets.FirstOrDefault(t => t.TicketId == ticketId);

        if (ticket == null)
        {
            return NotFound($"Ticket con ID {ticketId} no encontrado.");
        }

        return Ok(ticket);
    }

    // Obtener tickets por su email
    [HttpGet("emailCompra/{emailCompra}")]
    public ActionResult<IEnumerable<Ticket>> GetTicketsByEmail(string emailCompra)
    {
        var tickets = Tickets.Where(t => t.EmailCompra == emailCompra);

        if (tickets == null)
        {
            return NotFound($"Tickets con el correo {emailCompra} no encontrados.");
        }

        return Ok(tickets);
    }

    // Eliminar un ticket por su ID
    [HttpDelete("{ticketId}")]
    public IActionResult DeleteTicket(int ticketId)
    {
        // Buscar el ticket
        var ticket = Tickets.FirstOrDefault(t => t.TicketId == ticketId);
        if (ticket == null)
        {
            return NotFound($"Ticket con ID {ticketId} no encontrado.");
        }

        var sesion = DatosCines.Cines
            .SelectMany(c => c.Salas)
            .SelectMany(s => s.Sesiones)
            .FirstOrDefault(f => f.SesionId == ticket.SesionId);

        if (sesion != null && sesion.Butacas != null)
        {
            // Buscar la butaca asociada al ticket
            var butaca = sesion.Butacas.FirstOrDefault(b => b.ButacaId == ticket.ButacaId);
            if (butaca != null)
            {
                // Actualizar el estado de la butaca a "Disponible"
                butaca.Estado = "Disponible";
                butaca.TicketId = null;
            }
        }

        // Eliminar el ticket
        Tickets.Remove(ticket);

        return NoContent();
    }
}