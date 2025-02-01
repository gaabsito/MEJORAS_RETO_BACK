using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CineAPI.Service
{
    public interface ITicketService
    {
        Task<List<Ticket>> GetAllTicketsAsync();
        Task<Ticket?> GetTicketByIdAsync(int ticketId);
        Task<List<Ticket>> GetTicketsByEmailAsync(string email);
        Task<List<Ticket>> GetTicketsBySesionAsync(int sesionId);
        Task<int> AddTicketAsync(Ticket ticket);
        Task<bool> DeleteTicketAsync(int ticketId);
    }
}