using System;

namespace CineAPI.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public int SesionId { get; set; }
        public string NombreInvitado { get; set; }
        public string EmailCompra { get; set; }
        public int ButacaId { get; set; }
        public DateTime FechaDeCompra { get; set; }

        public Ticket() {}

        public Ticket(int sesionId, string nombreInvitado, string emailCompra, int butacaId, DateTime fechaDeCompra)
        {
            SesionId = sesionId;
            NombreInvitado = nombreInvitado;
            EmailCompra = emailCompra;
            ButacaId = butacaId;
            FechaDeCompra = fechaDeCompra;
        }
    }
}
