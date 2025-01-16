public class Ticket
{
    public int TicketId { get; set; }
    public int SesionId { get; set; }

    //public int UsuarioId { get; set; }
    public string NombreInvitado { get; set; }

    public string EmailCompra { get; set; }
    public int ButacaId { get; set; }
    public DateTime FechaDeCompra { get; set; }
}